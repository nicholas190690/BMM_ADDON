<?php
namespace Zircon\Modules\Checkpointservices\Models;
/*
 * Monday, 04 May 2020 14:45
 * Adapted from Caneservices Model Spta 
 *
 */
 use Zircon\hanaAdapterv2  as hanaAdapter;
 #use Zircon\hanaAdapter  as hanaAdapter;
 use Zircon\Modules\Checkpointservices\Models\Syncronize; 
 use Zircon\Modules\Checkpointservices\Models\Projectsetting;
 
 class Spta {
	/*
	 * Getting hana adapter 
	 */
	
	private $a_table;
	private $c_table;
	private $projectdata;	
	
	public function __construct(){
		#TEST_TABLE 
		#$this->a_table	= 'UAT_ADDON2';
		
		#LIVE TABLE 
		$this->a_table	= 'RMI_ADDON_MASTER';
		#$this->a_table	= 'RMI_ADDON_MASTER';
		
		#$this->c_table	= 'UAT2';
		//$this->c_table	= 'RMI';
		$this->c_table	= 'RMI';
		
		$projectsetting 	= new Projectsetting(); 
		$this->projectdata 	= $projectsetting->getprojectsetting();
	}
	
	public function getHanaConn(){
		$hdbodbc = new hanaAdapter();
		$conn = $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
		
		return $conn;
	}
	
	public function getSptaBroadcastData(){
		
		//untuk testing sementara kita ambil spta yang dicetak dalam sehari 
		$SQL 	= 'SELECT
		T2.SOL_NOKONTRAK ||\' - \'|| T2."SOL_NOKONLAM" AS "nomor_kontrak"
		,T0."DOCNUM" as "nomor_spta"
		,CASE IFNULL(T4."SOL_TIPE",\'\') WHEN  \'1\' THEN \'TRS\' WHEN  \'2\' THEN \'TRT\' ELSE \'\' END AS "tipe_petani"
		,T0."SOL_DATE" AS "tanggal_spta"
		,T0."SOL_REGISTRASI" AS "register"
		,T5."SOL_NAMA" AS "pemilik"
		,T4."SOL_DESA" AS "desa"
		,T4."SOL_PROVINSI" AS "provinsi"
		,t4."SOL_KABUPATEN" AS "kabupaten"
		,T4."SOL_KECAMATAN" AS "kecamatan"
		,t5."SOL_CARDCODEREFNAME" AS "koperasi_induk"
		,T0."SOL_APPROVE_TIMESTAMP" AS "printed_on"
		,T0."SOL_STATUS" AS "status_spta"
		FROM '.$this->a_table.'."SOL_SPTA" T0
		INNER JOIN '.$this->a_table.'."SOL_KONTRAK_D" T1 ON T0."BASEENTRY" = T1."DOCENTRY" AND T0."BASELINE" = T1."LINENUM"
		INNER JOIN '.$this->a_table.'."SOL_KONTRAK_H" T2 ON T1."DOCENTRY" = T2."DOCENTRY"
		INNER JOIN '.$this->a_table.'."SOL_PETAK" T3 ON T3."SOL_KDPETAK" = T1."SOL_KDPETAK" AND T3."LINENUM" = T1."SOL_LINEPETAK" AND T0."SOL_REGISTRASI" = T3."SOL_REGISTRASI"
		INNER JOIN '.$this->a_table.'."SOL_KEBUN" T4 ON T4."SOL_KDKEBUN" = T3."SOL_KDKEBUN" AND T4."SOL_REGISTRASI" = T3."SOL_REGISTRASI"
		INNER JOIN '.$this->a_table.'."SOL_PETANI" T5 ON T5."SOL_REGISTRASI" = T4."SOL_REGISTRASI"
		INNER JOIN '.$this->a_table.'."SOL_PRINTSPTA" T6 ON T6."SPTAENTRY" = T0."DOCENTRY"
		WHERE 
		--T0."SOL_STATUS"=\'A\' AND 
		T0."SOL_APPROVE_TIMESTAMP">=ADD_SECONDS (TO_TIMESTAMP (NOW()), -60*1440)
		ORDER BY T0."DOCENTRY" ASC';
		
		//echo $SQL;die;
		$adapter	= new hanaAdapter(); 
		$hcon		= $this->getHanaConn();	
		$recs		= $adapter->runcustomquery($SQL,$hcon);
		
		return $recs;
	}

	public function checksptanopol($nopol,$spta=''){
		$spta = trim($spta); 
		
		$SQL = 'SELECT * FROM RMI_ADDON_MASTER.SOL_SPTA ss 
		WHERE ss.SOL_STATUS IN (\'A\',\'I\',\'T\',\'M\') AND
		ss.SOL_CREATEDATE >= ADD_DAYS(CURRENT_DATE,-3)
		AND ss.SOL_NOPOL=\''.$nopol.'\''; 

		if($spta){
			$SQL .= ' AND ss.DOCNUM <> \''.$spta.'\''; 
		}

		$adapter	= new hanaAdapter(); 
		$hcon		= $this->getHanaConn();	
		$recs		= $adapter->runcustomquery($SQL,$hcon);

		return $recs; 
	}
	
	public function rundataquery($SQL,$autocommit=true){
		$hdbodbc 	= new hanaAdapter();
		$conn 		= $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
		$recs		= $hdbodbc->runcustomquery($SQL,$conn); 
		return $recs; 
	}
	
	private function getwconforexiststpa($findme,$criteria){
		$wcon 	= '';
		if($criteria=='farmer_id'){
			$wcon = ' AND T0."SOL_REGISTRASI"=\''.$findme.'\'';
		}elseif($criteria=='contract_id'){
			$wcon = ' AND T1."SOL_NOKONTRAK"=\''.$findme.'\'';
		}elseif($criteria=='identity_card'){
			$wcon = ' AND T0."SOL_KTP"=\''.$findme.'\'';
		}elseif($criteria=='harvesting_ticket'){
			$wcon = ' AND T3."DOCNUM"=\''.$findme.'\'';
		}elseif($criteria=='police_number'){
			$wcon = ' AND T3."SOL_NOPOL"=\''.$findme.'\'';
		}
		
		return $wcon;
	}
	
	private function checkimage($spta,$spta_date){
		
		$dirdate	= str_replace('-','/',$spta_date); 
		$checkdir 	= BASE_PATH.'/public/spta_upload/'.$dirdate.'/';
		$list 		= glob($checkdir.'*'.$spta.'*');
		$ret		= []; 
		if(count($list) > 0){
			
			foreach($list as $filename){
				
				$filename = str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$filename);
				if(preg_match('/brix/',$filename)){
					$ret['brix'] = $filename;	
				}else{
					$ret['truck'] = $filename;	
				}
				
			}
		}else{
			$ret['brix'] = '';
			$ret['truck'] = '';
		}
		
		return $ret;
	}
	
	/*
	 * ini yang kepake untuk broadcast SPTA 
	 */
	public function getSptaBrdData(){
		$sql='CALL RMI_MOBILE.MOB_SPTA()';
		
		$adapter=new hanaAdapter(); 
		$hcon=$this->getHanaConn();	
		$recs=$adapter->runcustomquery($sql,$hcon);
		
		return ($recs!=null)?$recs:'Data not found.';
	}
	
	public function getBroadCastSPTAUpdate(){
		$sql='CALL RMI_MOBILE.MOB_SPTA_UPDATE()';
		
		$adapter=new hanaAdapter(); 
		$hcon=$this->getHanaConn();	
		$recs=$adapter->runcustomquery($sql,$hcon);
		
		return ($recs!=null)?$recs:'Data not found.';
	}
	
	public function updateSPTA($sol_nopol,$sol_supir,$sol_kendaraan,$sol_pos,$no_spta,$brix,$gisdata=array(),$truck_status=0,$message=''){
		$no_spta	= trim($no_spta);
		$sol_nopol	= preg_replace('/s+/', '', $sol_nopol);
		
		$RMI_ISVALID	= 'N';
		
		if(count($gisdata) > 0){
			$checkpointcode = isset($sol_pos['checkpoint'])?$sol_pos['checkpoint']:'';
			$arremplasemen = ['00020','00019'];
			/*echo '<pre>';
			print_r($sol_pos).'<hr>';
			print_r($gisdata); 
			echo '</pre>';
			die('_SKIP_');*/
			
			$RMI_ISVALID 	= isset($gisdata['RMI_ISVALID'])?$gisdata['RMI_ISVALID']:'N';
			$RMI_TYPE 		= isset($gisdata['RMI_TYPE'])?$gisdata['RMI_TYPE']:'N';
			$RMI_AREAID 	= isset($gisdata['RMI_AREAID'])?$gisdata['RMI_AREAID']:'';
			$RMI_PROJECTID 	= isset($gisdata['RMI_PROJECTID'])?$gisdata['RMI_PROJECTID']:'';
			$RMI_BRIXSCG 	= isset($gisdata['RMI_BRIXSCG'])?$gisdata['RMI_BRIXSCG']:'';
			$RMI_ESTTEBANG 	= isset($gisdata['RMI_ESTTEBANG'])?$gisdata['RMI_ESTTEBANG']:'';
			
			$brix_scg     = isset($_POST['brix_scg'])?$_POST['brix_scg']:0;
			
			#die($brix_scg.'--OKE_'.$no_spta);
			if($brix_scg){
				$RMI_BRIXSCG = $brix_scg;
			}
			$sync = new Syncronize();	
			
			$checkus = $sync->showsptadata($no_spta,'harvesting_ticket',date('Ymd'));
			$sol_source = isset($checkus[0]['SOL_SOURCE'])?$checkus[0]['SOL_SOURCE']:'';
			/*echo '<pre>';
			print_r($brix_scg);
			echo '</pre>';
			die;*/
			
			$projectsetting 	= new Projectsetting(); 
			$this->projectdata 	= $projectsetting->getprojectsetting();
			//continue here 
			if(in_array($sol_source,$arremplasemen)){
				/*if($RMI_TYPE!='N'){
					if($RMI_BRIXSCG >=22){
						$RMI_ISVALID	= 'Y';
					}			
				}*/
				
				if(isset($this->projectdata[$RMI_TYPE])){
					$dtproject 	= $this->projectdata[$RMI_TYPE]; 
					$cp_brix   	= $dtproject['check_point_brix'];
					$em_brix	= $dtproject['emplasemen_point_brix'];
					
					
					if($RMI_BRIXSCG >=$em_brix){
						$RMI_ISVALID	= 'Y';
					}	
				}
				//remove when above experiment is working 
				/*if($RMI_TYPE=='D'){
					if($RMI_BRIXSCG >=24){
						$RMI_ISVALID	= 'Y';
					}			
				}elseif($RMI_TYPE=='H'){
					if($RMI_BRIXSCG >=23){
						$RMI_ISVALID	= 'Y';
					}			
				}*/
			}else{
				/*if($RMI_TYPE!='N'){
					if($brix >= 22 && $RMI_BRIXSCG >=22){ 
						$RMI_ISVALID	= 'Y';
					}
				}*/
				
				if(isset($this->projectdata[$RMI_TYPE])){
					$dtproject 	= $this->projectdata[$RMI_TYPE]; 
					$cp_brix   	= $dtproject['check_point_brix'];
					$em_brix	= $dtproject['emplasemen_point_brix'];
					
					
					if($brix >= $cp_brix && $RMI_BRIXSCG >= $em_brix){ 
						$RMI_ISVALID	= 'Y';
					}
				}
				
				//remove when above experiment is working 
				/*if($RMI_TYPE=='D'){
					if($brix >= 24 && $RMI_BRIXSCG >=24){ 
						$RMI_ISVALID	= 'Y';
					}
				}elseif($RMI_TYPE=='H'){
					if($brix >= 23 && $RMI_BRIXSCG >=23){ 
						$RMI_ISVALID	= 'Y';
					}
				}*/
				
			}
		}else{
			$RMI_ISVALID 	= isset($gisdata['RMI_ISVALID'])?$gisdata['RMI_ISVALID']:'N';
			$RMI_TYPE 		= isset($gisdata['RMI_TYPE'])?$gisdata['RMI_TYPE']:'N';
		}
		
		#$sol_nopol  = strtoupper($sol_nopol);
		#$sol_supir	= strtoupper($sol_supir);
		/*if($brix >= 19 && $RMI_BRIXSCG >=19){
			$RMI_ISVALID	= 'Y';
		}*/
		$sync = new Syncronize();
		
		if($no_spta){			
			
			if(!$RMI_PROJECTID){
				$RMI_PROJECTID = 0;
			}
			if(!$RMI_BRIXSCG){
				$RMI_BRIXSCG = 0;
			}
			
			/*$SQL = 'UPDATE '.$this->a_table.'."SOL_SPTA" T0
				SET 
				T0."SOL_NOPOL"=\''.$sol_nopol.'\',T0."SOL_SUPIR"=\''.$sol_supir.'\',
				T0."SOL_KENDARAAN"=\''.$sol_kendaraan.'\',T0."SOL_BRIX"=\''.$brix.'\'
				,T0."RMI_ISVALID"=\''.$RMI_ISVALID.'\'
				,T0."RMI_TYPE"=\''.$RMI_TYPE.'\'
				,T0."RMI_AREAID"=\''.$RMI_AREAID.'\'
				,T0."RMI_PROJECTID"=\''.$RMI_PROJECTID.'\'
				,T0."RMI_BRIXSCG"=\''.$RMI_BRIXSCG.'\'
				,T0."RMI_ESTTEBANG"=\''.$RMI_ESTTEBANG.'\'
				
				WHERE T0."DOCNUM"=\''.$no_spta.'\'';*/
			//trace berubah project 
			if($brix_scg > 0 || $RMI_BRIXSCG > 0){
				#die($brix_scg);	
				$SQL = 'UPDATE '.$this->a_table.'."SOL_SPTA" T0
					SET 
					T0."SOL_NOPOL"=\''.$sol_nopol.'\',T0."SOL_SUPIR"=\''.$sol_supir.'\',
					T0."SOL_KENDARAAN"=\''.$sol_kendaraan.'\',T0."SOL_BRIX"=\''.$brix.'\'
					,T0."RMI_ISVALID"=\''.$RMI_ISVALID.'\'
					,T0."RMI_TYPE"=\''.$RMI_TYPE.'\'
					,T0."RMI_AREAID"=\''.$RMI_AREAID.'\'
					,T0."RMI_PROJECTID"=\''.$RMI_PROJECTID.'\'
					,T0."RMI_BRIXSCG"=\''.$RMI_BRIXSCG.'\'
					,T0."TRUCK_STATUS"=\''.$truck_status.'\'
					WHERE T0."DOCNUM"=\''.$no_spta.'\' AND T0."SOL_STATUS"=\'A\'
					--1000_'.$message;				
			}else{
				$SQL = 'UPDATE '.$this->a_table.'."SOL_SPTA" T0
					SET 
					T0."SOL_NOPOL"=\''.$sol_nopol.'\',T0."SOL_SUPIR"=\''.$sol_supir.'\',
					T0."SOL_KENDARAAN"=\''.$sol_kendaraan.'\',T0."SOL_BRIX"=\''.$brix.'\'
					,T0."RMI_TYPE"=\''.$RMI_TYPE.'\'
					,T0."RMI_AREAID"=\''.$RMI_AREAID.'\'
					,T0."RMI_PROJECTID"=\''.$RMI_PROJECTID.'\'
					,T0."TRUCK_STATUS"=\''.$truck_status.'\'
					WHERE T0."DOCNUM"=\''.$no_spta.'\' AND T0."SOL_STATUS"=\'A\' 
					--1001_'.$message;
			}	
				
			#echo $SQL;die;
			$adapter	= new hanaAdapter(); 
			$hcon		= $this->getHanaConn();	
			$recs		= $adapter->runcustomquery($SQL,$hcon);
			#echo $SQL.'<br>';
			#die('_SKIP_');
			//Tendang data issue & issued at & issuer loc ke sol_pos
			/*
			$SQLpos = "CALL ".$this->a_table.".ITRMI_PUSH_GPSLOG(
				".$no_spta.",'".date('Y-m-d H:i:s')."',
				".(int)date('Hi').",'".$sol_pos['checkpoint']."',
				'".$sol_pos['longitude']."','".$sol_pos['location']."',
				'".$sol_pos['latitude']."')";
			*/
			//echo $SQLpos;die();
			//$sqlinspos = $adapter->runcustomquery($SQLpos,$hcon);
		}
		
		return "success";
	}
	
	public function pushspta($dt)
	{
		$ret	= [];
		
		$sync = new Syncronize();
		
		/*echo "<pre>";
		print_r($dt);
		echo "</pre>";*/
		
		if(count($dt) > 0){			
			$DOCNUM					= $dt['DOCNUM'];
			$SOL_REGISTRASI 		= $dt['SOL_REGISTRASI'];
			$BASEENTRY 				= $dt['BASEENTRY'];
			$BASELINE				= $dt['BASELINE'];
			$SOL_QUANTITY 			= $dt['SOL_QUANTITY'];
			$SOL_STATUS 			= $dt['SOL_STATUS'];
			$SOL_NOPOL 				= $dt['SOL_NOPOL'];
			$SOL_SUPIR 				= $dt['SOL_SUPIR'];
			$SOL_KENDARAAN 			= $dt['SOL_KENDARAAN'];
			$SOL_DATE 				= $dt['SOL_DATE'];
			$SOL_EXPIRY 			= $dt['SOL_EXPIRY'];
			$SOL_BRIX				= $dt['SOL_BRIX'];
			$SOL_CREATEDATE 		= $dt['SOL_CREATEDATE'];
			$SOL_SOURCE 			= $dt['SOL_SOURCE'];
			$SOL_APPROVE_TIMESTAMP 	= $dt['SOL_APPROVE_TIMESTAMP'];
			//18 of May 2021 penyesuaian untuk project drone & tebu manis
			$RMI_ISVALID 	= $dt['RMI_ISVALID'];
			$RMI_TYPE 		= $dt['RMI_TYPE'];
			$RMI_AREAID 	= $dt['RMI_AREAID'];
			$RMI_PROJECTID 	= $dt['RMI_PROJECTID'];
			$RMI_BRIXSCG 	= $dt['RMI_BRIXSCG'];
			$RMI_ESTTEBANG 	= $dt['RMI_ESTTEBANG'];
			$TRUCK_STATUS 	= $dt['TRUCK_STATUS'];
			
			$hdbodbc 				= new hanaAdapter();
			$conn 					= $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
			
			$hdbodbc->autocommitcontrol($conn,false);
			
			$SQL					= "CALL ".$this->a_table.".ITRMI_PUSH_SPTA(".$DOCNUM.",'".$SOL_REGISTRASI."',".$BASEENTRY.",".$BASELINE;
			$SQL				   .= ",".$SOL_QUANTITY.",'".$SOL_STATUS."','".$SOL_NOPOL."','".$SOL_SUPIR."','".$SOL_KENDARAAN."','".$SOL_DATE."'";
			$SQL				   .= ",'".$SOL_EXPIRY."','".$SOL_BRIX."','".$SOL_SOURCE."'";
			$SQL				   .= ",'".$RMI_ISVALID."','".$RMI_TYPE."','".$RMI_AREAID."'";
			$SQL				   .= ",'".$RMI_PROJECTID."','".$RMI_BRIXSCG."','".$RMI_ESTTEBANG."',".$TRUCK_STATUS;
			$SQL				   .= ")";
			
			#echo $SQL;die;
			//run query 
			$sqlret					= $hdbodbc->runcustomquery($SQL,$conn); 
			$sync->savecheckpoint($DOCNUM,'OUT',$SOL_SOURCE,$dt['SOL_LONGITUDE'],$dt['SOL_LATITUDE'],$dt['SOL_LOCATION']);
			
			/*echo '<pre>';
			print_r($sqlret);
			echo $SQL;
			echo '<pre>';
			die('_SKIP_REK_');*/
			
			if(isset($sqlret[0][':1'])){
				$DOCENTRYCHECK			= $sqlret[0][':1'];	
				$check 					= $this->validatespta($DOCENTRYCHECK);
				
				/*echo "<pre>";
				print_r($check);
				echo "</pre>";*/
				
				if($check=='valid'){
					//commit transaction 
					$hdbodbc->committransaction($conn);
					//when success creating SPTA (harvesting ticket) return data of SPTA checkexistingspta
					//in mobile application it will be updated by check point officer or parking area officer 
					
					$ret = $this->checkexistingsptabyreg('2-'.$DOCNUM,'harvesting_ticket',date('Ymd'));
					/*echo '<pre>';
					print_r($ret);
					echo '</pre>';
					die('_SKIP_');*/
				}else{
					//roll back transaction 
					$hdbodbc->rollbacktransaction($conn);
				}				
			}else{
				$ret = [];
			}
		}
		
		/*echo "<pre>";
		print_r($ret);
		echo "</pre>";
		die('_HAI_');*/
		return $ret;
	}
	
	public function insertspta($dt)
	{
		#die('OK');
		
		$ret	= [];
		if(count($dt) > 0){			
			$DOCNUM					= $dt['DOCNUM'];
			$SOL_REGISTRASI 		= $dt['SOL_REGISTRASI'];
			$BASEENTRY 				= $dt['BASEENTRY'];
			$BASELINE				= $dt['BASELINE'];
			$SOL_QUANTITY 			= $dt['SOL_QUANTITY'];
			$SOL_STATUS 			= $dt['SOL_STATUS'];
			$SOL_NOPOL 				= $dt['SOL_NOPOL'];
			$SOL_SUPIR 				= $dt['SOL_SUPIR'];
			$SOL_KENDARAAN 			= $dt['SOL_KENDARAAN'];
			$SOL_DATE 				= $dt['SOL_DATE'];
			$SOL_EXPIRY 			= $dt['SOL_EXPIRY'];
			$SOL_CREATEDATE 		= $dt['SOL_CREATEDATE'];
			$SOL_SOURCE 			= $dt['SOL_SOURCE'];
			$SOL_APPROVE_TIMESTAMP 	= $dt['SOL_APPROVE_TIMESTAMP'];
			
			//processing insert to database 
			$hdbodbc 				= new hanaAdapter();
			$conn 					= $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
			
			//start transaction 
			$hdbodbc->autocommitcontrol($conn,false);
			
			$SQL					= "CALL ".$this->a_table.".SOL_INSERT_SPTA(".$DOCNUM.",'".$SOL_REGISTRASI."',".$BASEENTRY.",".$BASELINE;
			$SQL				   .= ",".$SOL_QUANTITY.",'".$SOL_STATUS."','".$SOL_NOPOL."','".$SOL_SUPIR."','".$SOL_KENDARAAN."','".$SOL_DATE."'";
			$SQL				   .= ",'".$SOL_EXPIRY."','".$SOL_SOURCE."'";
			$SQL				   .= ")";
			#echo $SQL;die;
			//run query 
			$sqlret					= $hdbodbc->runcustomquery($SQL,$conn); 
			
			/*echo '<pre>';
			print_r($sqlret);
			echo '<pre>';
			die('_SKIP_REK_');*/
			
			if(isset($sqlret[0][':1'])){
				$DOCENTRYCHECK			= $sqlret[0][':1'];	
				$check 					= $this->validatespta($DOCENTRYCHECK);
				#die($check);
				if($check=='valid'){
					//commit transaction 
					$hdbodbc->committransaction($conn);
					//when success creating SPTA (harvesting ticket) return data of SPTA checkexistingspta
					//in mobile application it will be updated by check point officer or parking area officer 
					
					$ret = $this->checkexistingspta($DOCNUM,'harvesting_ticket',date('Ymd'));
					
					/*echo '<pre>';
					print_r($ret);
					echo '</pre>';
					die('_SKIP_');*/
				}else{
					#die('rollback');
					//roll back transaction 
					$hdbodbc->rollbacktransaction($conn);
				}				
			}
		}
		
		return $ret;
	}
	
	/*
	 * VALIDATING SPTA NUMBER BEFORE COMMIT TRANSACTION 
	 */
	public function validatespta($docentry){
		$SQL 	= "CALL ".$this->a_table.".SOL_SP_VALIDASI ('2','A',".$docentry.")";
		$dt 	= $this->rundataquery($SQL); 
		$valid	= 'not-valid';
		/*echo '<pre>';
		print_r($dt); 
		echo '</pre>';
		die('_SKIP_');*/
		if(count($dt) > 0){
			if(isset($dt[0]['Error_Message'])){
				/*$checkus 	= $dt[0]['Error_Message'];
				if($checkus == 'Ok'){
					$valid  =  'valid';
				}*/
				$checkus 	= intval($dt[0]['Error']);
				if($checkus < 0){
					$valid	= 'not-valid';
				}else{
					$valid  =  'valid';
				}
			}
		}
		#echo $valid;die;
		return $valid;
	}
	
	public function checkexistingspta($findme,$criteria,$day,$check_point=''){
		$recs		= [];
		$expiration = date('Ymd',strtotime('+1 day', strtotime($day)));
		if($findme && $criteria && $day){
			//--added on 05/05/2020--
			//for ommiting 2- in spta number 
			if($criteria=='harvesting_ticket'){
				$findme	= str_replace('2-','',$findme); 
				$sptadate	= substr(date('Y'),0,2).substr($findme,0,6);
				$expiration	= date('Ymd',strtotime('+1 day', strtotime($sptadate)));
			}
			//-----------------------
			$wcon			= $this->getwconforexiststpa($findme,$criteria);
			$checkpointcon	= (!empty($check_point))
							?' AND T3."SOL_SOURCE"='.$check_point
							:'';
			$myyear			= substr($day,0,4);
			/*$SQL	= 'SELECT 
				T0."SOL_REGISTRASI" AS "farmer_id"
				,T1."SOL_NOKONTRAK" AS "contract_id"
				,T0."SOL_NAMA" AS "farmer_name"
				,T0."SOL_KTP" AS "identity_card"
				,T0."SOL_CARDCODEREFNAME" AS "business_partner"
				,T0."SOL_DESA" AS "village"
				,T0."SOL_KECEMATAN" AS "district" 
				,T0."SOL_KABUPATEN" AS "regency"
				,TO_DECIMAL(T0."SOL_TOTALLAHAN",10,0) AS "land_area"
				,TO_DECIMAL((T2."SOL_QUANTITY"/1000),10,0) AS "tonnage"
				,T3."DOCNUM" AS "harvesting_ticket"
				,T3."SOL_SUPIR" AS "driver"
				,T3."SOL_NOPOL" AS "police_number"
				,T3."SOL_KENDARAAN" AS "vehicle_type"
				,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,TO_DECIMAL(IFNULL(T3."SOL_BRIX",0),10,0) AS "brix"
				,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_FROM")=\''.$myyear.'\') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\' AND T21."SOL_QUANTITY" > 0) AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
				WHERE YEAR(T1."SOL_FROM")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 
				AND T2."SOL_QUANTITY" > 0
				AND T3."SOL_NOPOL"=\'\'
				AND T3."SOL_SUPIR"=\'\'
				AND T3."SOL_EXPIRY" = \''.$expiration.'\''.$wcon;*/
			$SQL	= 'SELECT 
				T0."SOL_REGISTRASI" AS "farmer_id"
				,T1."SOL_NOKONTRAK" AS "contract_id"
				,T0."SOL_NAMA" AS "farmer_name"
				,T0."SOL_KTP" AS "identity_card"
				,T0."SOL_CARDCODEREFNAME" AS "business_partner"
				,T0."SOL_DESA" AS "village"
				,T0."SOL_KECEMATAN" AS "district" 
				,T0."SOL_KABUPATEN" AS "regency"
				,TO_DECIMAL(T0."SOL_TOTALLAHAN",10,0) AS "land_area"
				,TO_DECIMAL((T2."SOL_QUANTITY"/1000),10,0) AS "tonnage"
				,T3."DOCNUM" AS "harvesting_ticket"
				,T3."SOL_DATE" AS "tgl_spta"
				,T3."SOL_SUPIR" AS "driver"
				,T3."SOL_NOPOL" AS "police_number"
				,T3."SOL_KENDARAAN" AS "vehicle_type"
				,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,TO_DECIMAL(IFNULL(T3."SOL_BRIX",0),10,0) AS "brix"
				,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_FROM")=\''.$myyear.'\') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
				WHERE YEAR(T1."SOL_FROM")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 
				AND T3."SOL_NOPOL"=\'\'
				AND T3."SOL_SUPIR"=\'\'
				AND T3."SOL_EXPIRY" = \''.$expiration.'\''.$wcon.$checkpointcon.'  ORDER BY T3."DOCENTRY" DESC LIMIT 1';
			
			#echo $expiration.'<hr>';
			#echo $SQL;die;	
			$recs		=	$this->rundataquery($SQL); 
			if(count($recs) > 0){
				if(!empty($recs[0]['driver']) && !empty($recs[0]['police_number']) && !empty($recs[0]['brix'])){
					$recs[0]['show_button'] = 1;
				}else{
					$recs[0]['show_button'] = 0;
				}
				$imagechecks = $this->checkimage($recs[0]['harvesting_ticket'],$recs[0]['tgl_spta']);	
				if(count($imagechecks) > 0){
					$recs[0]['images']	= $imagechecks;
				}	
			}
		}	
		return $recs;
	}
	
	public function checkexistingsptabyreg($findme,$criteria,$day,$check_point=''){
		$recs=[];
		
		$expiration = date('Ymd',strtotime('+1 day', strtotime($day)));
		if($findme && $criteria && $day){
			//--added on 05/05/2020--
			//for ommiting 2- in spta number 
			if($criteria=='harvesting_ticket'){
				$findme	= str_replace('2-','',$findme); 
				$sptadate	= substr(date('Y'),0,2).substr($findme,0,6);
				$expiration	= date('Ymd',strtotime('+1 day', strtotime($sptadate)));
			}
			//-----------------------
			$wcon			= $this->getwconforexiststpa($findme,$criteria);
			$checkpointcon	= (!empty($check_point))
							?' AND T3."SOL_SOURCE"='.$check_point
							:'';
			$myyear			= substr($day,0,4);
			
			if($criteria=='harvesting_ticket'){
				$SQL	='
				SELECT
				T0."DOCNUM" AS "harvesting_ticket"
				,T0."SOL_DATE" AS "tgl_spta"
				,T0."SOL_SUPIR" AS "driver"
				,T0."SOL_NOPOL" AS "police_number"
				,T0."SOL_KENDARAAN" AS "vehicle_type"
				,TO_DECIMAL(IFNULL(T0."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,TO_DECIMAL(IFNULL(T0."SOL_BRIX",0),10,0) AS "brix"
				,TO_DECIMAL(IFNULL(T0."SOL_POL",0),10,0) AS "pol"
				,T0.TRUCK_STATUS
				FROM '.$this->a_table.'."SOL_SPTA" T0
				LEFT JOIN '.$this->a_table.'."SOL_PETANI" T1 ON T0."SOL_REGISTRASI"=T1."SOL_REGISTRASI"
				WHERE T0."DOCNUM"=\''.$findme.'\'
				AND T0.DOCNUM LIKE \''.$day[2].$day[3].$day[4].$day[5].'%\'
				AND T0.SOL_NOPOL <> \'\'
				AND T0.SOL_SUPIR <> \'\'
				AND T1."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T0."SOL_EXPIRY" = \''.$expiration[0].$expiration[1].$expiration[2].$expiration[3].$expiration[4].$expiration[5].$expiration[6].$expiration[7].'\'';
				
			}else if($criteria=='farmer_id'){
				$SQL	='
				SELECT
				T0."DOCNUM" AS "harvesting_ticket"
				,T0."SOL_DATE" AS "tgl_spta"
				,T0."SOL_SUPIR" AS "driver"
				,T0."SOL_NOPOL" AS "police_number"
				,T0."SOL_KENDARAAN" AS "vehicle_type"
				,TO_DECIMAL(IFNULL(T0."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,TO_DECIMAL(IFNULL(T0."SOL_BRIX",0),10,0) AS "brix"
				,TO_DECIMAL(IFNULL(T0."SOL_POL",0),10,0) AS "pol"
				,T0.TRUCK_STATUS
				FROM '.$this->a_table.'."SOL_SPTA" T0
				LEFT JOIN '.$this->a_table.'."SOL_PETANI" T1 ON T0."SOL_REGISTRASI"=T1."SOL_REGISTRASI"
				WHERE T0."SOL_REGISTRASI"=\''.$findme.'\'
				AND T0.DOCNUM LIKE \''.$day[2].$day[3].$day[4].$day[5].'%\'
				AND T0.SOL_NOPOL <> \'\'
				AND T0.SOL_SUPIR <> \'\'
				AND T1."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T0."SOL_EXPIRY" = \''.$expiration[0].$expiration[1].$expiration[2].$expiration[3].$expiration[4].$expiration[5].$expiration[6].$expiration[7].'\'';
			}
			$recs		=	$this->rundataquery($SQL);
			
			//print_r($recs);
			/*echo "<pre>";
			print_r($SQL);
			echo "</pre>";
			echo "<pre>";
			print_r($recs);
			echo "</pre>";die;*/
			
			if(count($recs) > 0){
				
				if(!empty($recs[0]['driver']) && !empty($recs[0]['police_number']) && !empty($recs[0]['brix'])){
					$recs[0]['show_button'] = 1;
				}else{
					$recs[0]['show_button'] = 0;
				}
				$imagechecks = $this->checkimage($recs[0]['harvesting_ticket'],$recs[0]['tgl_spta']);	
				if(count($imagechecks) > 0){
					$recs[0]['images']	= $imagechecks;
				}	
			}
		}
		return $recs;
	}
	
	/* Added 14/07/2020
	 * checking spta duration by date
	 *
	 */
	 
	public function checksptabydate($findme,$criteria,$vehicle_no,$driver_name,$day)
	{
		$out='';
		
		if($criteria=='contract_id'){
			$contract_nr=$findme;
			$conditions=" WHERE T0.SOL_NOKONTRAK='".$contract_nr."'";
			
			$SQL='SELECT T0."SOL_REGISTRASI" AS "register" FROM '.$this->a_table.'."SOL_KONTRAK_H" T0 '.$conditions.'LIMIT 1';
			
			//echo $SQL;
			
			$recs=$this->rundataquery($SQL);
			
			#echo "<pre>";
			#print_r($recs);
			#echo "</pre>";
			//die();
			$register=$recs[0]['register'];
		}
		
		//Patch 20200802__2044
		//added identity card as update-helper criteria
		
		if($criteria=='identity_card')
		{
			$id_card=$findme;
			$conditions=" WHERE T0.SOL_KTP='".$id_card."'";
			
			$SQL='SELECT T0."SOL_REGISTRASI" AS "register" FROM '.$this->a_table.'."SOL_PETANI" T0 '.$conditions.'LIMIT 1';

			$recs=$this->rundataquery($SQL);
			$register=$recs[0]['register'];
		}
		
		
		if($criteria=='farmer_id'){
			$register=$findme;
		}
		if(isset($register)){
			$conditions=" WHERE T0.SOL_REGISTRASI='".$register."' AND T0.SOL_NOPOL='".$vehicle_no."'".
					" AND ABS(SECONDS_BETWEEN(T0.SOL_APPROVE_TIMESTAMP, CURRENT_TIMESTAMP)) < 7200".
					" AND T0.SOL_STATUS='A'".
					"";
				
			$SQL='SELECT T0.DOCNUM AS "spta_num", T0.SOL_APPROVE_TIMESTAMP AS "appr_time" FROM '.$this->a_table.
				'."SOL_SPTA" T0 '.$conditions.' ORDER BY "spta_num" DESC LIMIT 1';
			#die($SQL);
			$recs		=	$this->rundataquery($SQL);
		
		}else{
			$recs = array();
		}
		
		if(count($recs)>0){
			$out=$recs[0];
		}else{
			$out=['spta_num'=>'','appr_time'=>''];
		}			
		
		return $out;
	}
	
	
 }
