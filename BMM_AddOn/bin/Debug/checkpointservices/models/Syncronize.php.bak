<?php 
namespace Zircon\Modules\Checkpointservices\Models;
/*
 * Bismillahirrahmanirrahim...									Blitar, 25 of September 2019 
 * This model is created for SptaRestApi Controller 
 */
 #use Zircon\hanaAdapter as hanaAdapter;
 use Zircon\hanaAdapterv2 as hanaAdapter;
 
 class Syncronize {
	
	private $a_table;
	private $c_table; 
	
	public function __construct(){
		#TEST_TABLE		
		#$this->a_table	= 'UAT_ADDON2';
		
		#LIVE TABLE 
		$this->a_table	= 'RMI_ADDON_MASTER';
		
		#$this->c_table	= 'UAT2';
		
		$this->c_table	= 'RMI';
	}
	
	/*
	 * Blitar 28/04/2020  					Compact code for query 
	 */
	public function rundataquery($SQL,$autocommit=true){
		#die($SQL);
		$hdbodbc 	= new hanaAdapter();
		$conn 		= $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
		$recs		= $hdbodbc->runcustomquery($SQL,$conn); 
		return $recs; 
	}
	
	/*
	 * GET LAST SPTA NUM IN THE DAY (SOL_SPTA)
	 */	
	public function getNextSptaNumber($day){
		
		$SQL 	= 'SELECT T0."DOCENTRY",T0."DOCNUM" FROM '.$this->a_table.'."SOL_SPTA" T0
		WHERE T0."SOL_DATE"=\''.$day.'\'ORDER BY T0."DOCNUM" DESC LIMIT 1'; 
		
		#echo $SQL;die;
		$recs		=	$this->rundataquery($SQL); 
		//-------------
		$lastsptanum	= 0;
		$serialnumber	= '';
		if(count($recs) > 0){
			//when thereis SPTA printed before 
			$lastsptanum	= isset($recs[0]['DOCNUM'])?$recs[0]['DOCNUM']:0;
			$serialnumber	= substr($lastsptanum,-4);
			$nextnumber		= $serialnumber+1;
			$nextnumber		= sprintf("%04d", $nextnumber);
			$nextsptanumber	= date('ymd').$nextnumber;
		}else{
			//first SPTA print
			$nextsptanumber	= $day.'0001';
			//need substr to cut first two letter 
			$nextsptanumber	= substr($nextsptanumber,2);
		}
		#echo $nextsptanumber;die;
		return $nextsptanumber; 
		//-------------
		#return $recs; 
	}
	
	/*
	 * CHECK EXISTING SPTA FOR FARMER 
	 * variables : 
	 * 	@findme 	: what need to be found 
	 *  @criteria 	: find by filter 
	 *  @day		: harvesting ticket date 
	 * -------------------------------------
	 * year on table SOL_KONTRAK_H depend on @day year 
	 */
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
				,T3."TRUCK_STATUS"
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

	//test function
	public function checkfarmerData($findme,$criteria,$day,$check_point=''){
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
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_FROM")=\''.$myyear.'\') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				WHERE YEAR(T1."SOL_FROM")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1';
			
			#echo $expiration.'<hr>';
			#echo $SQL;die;	
			$recs		=	$this->rundataquery($SQL); 
			if(count($recs) > 0){
				if(!empty($recs[0]['driver']) && !empty($recs[0]['police_number']) && !empty($recs[0]['brix'])){
					$recs[0]['show_button'] = 1;
				}else{
					$recs[0]['show_button'] = 0;
				}
			}
		}	
		return $recs;
	}
	
	/*
	 * SPTA CHECK 
	 */
	public function showsptadata($findme,$criteria,$day){
		$expiration = date('Ymd',strtotime('+1 day', strtotime($day)));
		$recs		= [];
		#die('_OKE_');
		if($findme && $criteria && $day){
			//--added on 05/05/2020--
			//for ommiting 2- in spta number 
			if($criteria=='harvesting_ticket'){
				$findme		= str_replace('2-','',$findme); 
				$sptadate	= substr(date('Y'),0,2).substr($findme,0,6);
				$expiration	= date('Ymd',strtotime('+1 day', strtotime($sptadate)));
				
			}
			//-----------------------
			$wcon			= $this->getwconforexiststpa($findme,$criteria);
			$myyear			= substr($day,0,4);
			//---added on 19 Mei 2020 
			$insentifcheck 	= $this->insentifcheck($findme,$criteria); 
			$pospantaufield	= $insentifcheck['pospantau'];
			$pospantaucond	= $insentifcheck['wcond'];
			/*echo '<pre>';
			print_r($insentifcheck);
			echo '</pre>';
			die('_SKIP_');*/
			
			//---------------------------------
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
					LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\' AND T21."SOL_QUANTITY" > 0) AS T2
					ON T2."DOCENTRY"=T1."DOCENTRY" 
					LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
					LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
					WHERE YEAR(T1."SOL_FROM")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
					AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
					AND T1."ROW_NUM"=1 
					AND T2."SOL_QUANTITY" > 0 AND T3."SOL_EXPIRY" = \''.$expiration.'\''.$wcon.' LIMIT 1';*/
			
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
					,T3."RMI_ISVALID"
					,T3."RMI_TYPE"
					,T3."RMI_AREAID"
					,T3."RMI_PROJECTID"
					,T3."RMI_BRIXSCG"
					,T3."RMI_ESTTEBANG"
					,T7."Name" AS "jenis_truck"
					,T6."RMI_FLAG"
					,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
					,TO_DECIMAL(IFNULL(T3."SOL_BRIX",0),10,0) AS "brix"
					,CASE IFNULL(T8."SOL_TIPE",\'\') WHEN  \'1\' THEN \'TRS\' WHEN  \'2\' THEN \'TRT\' ELSE \'-\' END AS "TipeKebun"
					,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"'.$pospantaufield.'
					,T6.CODE
					,T3.SOL_SOURCE
					,T3."TRUCK_STATUS"
					,IFNULL(T9.RITASE,0) AS "RITASE"
					,IFNULL(T9.RITASEUSED,0) AS "RITASEUSED"
					,RMI_ADDON_MASTER.COUNTRITASE(IFNULL(RMI_AREAID,\'\')
					,IFNULL(RMI_PROJECTID,0),IFNULL(RMI_TYPE,\'\')) AS "TOTALSPTAAREAID"
					FROM '.$this->a_table.'."SOL_PETANI" T0 
					LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
					FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")=\''.$myyear.'\') AS T1
					ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
					LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
					ON T2."DOCENTRY"=T1."DOCENTRY" 
					LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
					LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
					LEFT JOIN '.$this->a_table.'."SOL_POS" T5 ON T5."SOL_SPTA"=T3."DOCNUM" 
					LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T6 ON T6."CODE"=T5."SOL_POSPANTAU" 
					LEFT JOIN '.$this->c_table.'."@SOL_JENISKENDARAAN" T7 ON CAST(T3."SOL_KENDARAAN" AS NVARCHAR(50)) = CAST(T7."Code" AS NVARCHAR(50))
					LEFT JOIN '.$this->a_table.'."SOL_KEBUN" T8 ON T8."SOL_REGISTRASI" = T0."SOL_REGISTRASI"
					LEFT JOIN '.$this->a_table.'."RMI_MST_PROJECT" T9 ON T9.AREAID = T3.RMI_AREAID  
					AND T9.PROJECTYPE = T3.RMI_TYPE AND T9.PROJECTID = T3.RMI_PROJECTID AND T3."SOL_REGISTRASI" = T9."SOL_REGISTRASI"
					WHERE '.$pospantaucond.'YEAR(T1."SOL_TO")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
					AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
					AND T1."ROW_NUM"=1 
					AND T3."SOL_STATUS"=\'A\' AND T3.SOL_DATE >= ADD_DAYS(CURRENT_DATE ,-4)'.$wcon.' ORDER BY T3."DOCENTRY" DESC LIMIT 1';
					//AND T3."SOL_STATUS" IN (\'A\',\'M\')
			#die('_AHA_');
			#echo $SQL;die('_SKIP_');
			$recs		=	$this->rundataquery($SQL); 
			/*echo '<pre>';
			echo $SQL;
			echo '</pre>';
			die('_SKIP_');*/
			if(count($recs) > 0){
				$recs[0]['RMI_BRIXSCG'] = number_format($recs[0]['RMI_BRIXSCG'],0,',','.');
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
			/*echo '<pre>';
			print_r($SQL);
			echo '</pre>';
			print_r($recs);
			die();*/
		}		
				
		return $recs;
	} 
	/*
	 * checking image exist, 08 05 2020  
	 */
	private function checkimage($spta,$spta_date){
		
		$dirdate	= str_replace('-','/',$spta_date); 
		#$checkdir 	= BASE_PATH.'/public/spta_upload/'.$dirdate.'/';
		$checkdir 	= '/var/www/test.rejosomanisindo.com/rmi_module/public/spta_upload/'.$dirdate.'/';
		$list 		= glob($checkdir.'*'.$spta.'*');
		$ret		= [];
		
        $ret['brix'] = '';
		$ret['truck']= '';
		$ret['vregister']= '';
		$ret['brix_scg_photo'] = '';
		$ret['truck_scg_photo'] = '';
		
		/*if(count($list) <= 0 ){
			$checkdir 	= BASE_PATH.'/public/spta_upload/'.$dirdate.'/';
			$list 		= glob($checkdir.'*'.$spta.'*');
		}*/

		if(count($list) > 0){
			
			foreach($list as $filename){
				
				#$filename = str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$filename);
				$filename = str_replace('/var/www/test.rejosomanisindo.com/rmi_module/public/','https://apps.rejosomanisindo.com/',$filename);
				if(preg_match('/brix/',$filename) && !preg_match('/brix_scg_photo/',$filename)){
					$ret['brix'] = $filename;	
				
				/*
				else{
					$ret['truck'] = $filename;	
				}*/
				
				//modified 06 juli 2020 16:40
				}else if(preg_match('/truck/',$filename) && !preg_match('/truck_scg_photo/',$filename) ){
					$ret['truck'] = $filename;
				}else if(preg_match('/vregister/',$filename)){
						//vehicle register image assert
						$ret['vregister'] = $filename;
					/*
					}else{
						$ret['vregister'] = '';
					}*/
				}else if(preg_match('/brix_scg_photo/',$filename)){
					$ret['brix_scg_photo'] = $filename;
				}else{
					$ret['truck_scg_photo'] = $filename;
				}	
			}
		}else{
			$ret['brix'] 			= '';
			$ret['truck'] 			= '';
			//added vehicle register image path
			$ret['vregister'] 		= '';
			$ret['brix_scg_photo'] 	= '';
			$ret['truck_scg_photo'] = ''; 
		}
		
		return $ret;
	}	
	
	/*
	 * farmer validation 
	 * Blitar, 22 of April 2020 
	 * @day format : yymmdd
	 */
	public function validatefarmer($findme,$criteria,$day,$validret=true){
		#die('_AHA_'.__LINE__.$criteria);
		$arrcriteria	= ['farmer_id','contract_id','identity_card'];
		$validcheck 	= 'not-valid';
		$myyear			= substr($day,0,4);
		$ret			= 'not-valid';
		if(in_array($criteria,$arrcriteria)){
			$wcon		= $this->getwconforexiststpa($findme,$criteria);
			/*$SQL 		= 'SELECT 
				T0."SOL_REGISTRASI"
				,T2."DOCENTRY" AS "BASEENTRY"
				,T2."LINENUM" AS "BASELINE"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_H" T1
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")='.$myyear.') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_D" T2 
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\' AND T21."SOL_QUANTITY" > 0) AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				WHERE YEAR(T1."SOL_TO")='.$myyear.' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 
				AND T2."SOL_QUANTITY" > 0'.$wcon;*/
				$SQL 		= 'SELECT 
				T0."SOL_REGISTRASI"
				,T2."DOCENTRY" AS "BASEENTRY"
				,T2."LINENUM" AS "BASELINE"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_H" T1
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")='.$myyear.') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_D" T2 
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				WHERE YEAR(T1."SOL_TO")='.$myyear.' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 '.$wcon;	

			#$SQL = 'SELECT * FROM RMI_ADDON_TEST."SOL_PETANI" T0 WHERE T0."SOL_KTP"=\'3505160303550002\'';	
			#echo $SQL.'<hr>';	
			#die;
			$recs		= $this->rundataquery($SQL); 
			/*echo '<pre>';
			print_r($SQL); 
			echo '</pre>';
			die('_AHA_');*/
			if(count($recs) > 0 && $validret == true){
				$ret	= 'valid';
			}else if(count($recs) > 0 && $validret == false){
				$ret 	= $recs; 
			}				
		}
		return $ret;
	}
	/**
	 * added on 14 of May 2023 
	 * untuk bedakan message bp & kontrak 
	 */
	public function validatefarmerv2($findme,$criteria,$day){
		#die('_AHA_'.__LINE__.$criteria);
		$arrcriteria	= ['farmer_id','contract_id','identity_card'];
		$validcheck 	= 'not-valid';
		$myyear			= substr($day,0,4);
		$ret			= 'not-valid';
		if(in_array($criteria,$arrcriteria)){
			$wcon		= $this->getwconforexiststpa($findme,$criteria);
			/*$SQL 		= 'SELECT 
				T0."SOL_REGISTRASI"
				,T2."DOCENTRY" AS "BASEENTRY"
				,T2."LINENUM" AS "BASELINE"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_H" T1
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")='.$myyear.') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_D" T2 
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\' AND T21."SOL_QUANTITY" > 0) AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				WHERE YEAR(T1."SOL_TO")='.$myyear.' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 
				AND T2."SOL_QUANTITY" > 0'.$wcon;*/
				$SQL 		= 'SELECT 
				T0."SOL_REGISTRASI"
				,T2."DOCENTRY" AS "BASEENTRY"
				,T2."LINENUM" AS "BASELINE"
				,T0.SOL_CARDCODEREF
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_H" T1
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")='.$myyear.') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				--LEFT JOIN '.$this->a_table.'."SOL_KONTRAK_D" T2 
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				WHERE YEAR(T1."SOL_TO")='.$myyear.' AND T1."SOL_STATUS"=\'O\' 
				AND T1."ROW_NUM"=1 '.$wcon;	

			
			$recs		= $this->rundataquery($SQL); 
			/*echo '<pre>';
			print_r($SQL);
			print_r($recs);
			echo '</pre>';
			die('_SKIP_');*/
			if(count($recs) > 0){
				if(isset($recs[0]['SOL_CARDCODEREF'])){
					$bp = $recs[0]['SOL_CARDCODEREF']; 
					if($bp=='VT00001'){
						$ret = 'dummy-bp'; 	
					}else{
						$ret = 'valid-data';
					}
				}else{
					$ret = 'no-contract'; 
				}				
			}else{
				$ret = 'no-contract'; 
			}
		}
		return $ret;
	}
	 //-----------------------
	/*
	 * Getting condition for exisiting SPTA 
	 */
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
	/*
	 * CHECKING SPTA INSENTIF, untuk yang lewat pos pantau atau bukan 
	 */
	private function insentifcheck($findme,$criteria){
		$CHECKPOINTCOUNT	= 0;
		$PARKINGAREACOUNT	= 0;
		$WCOND				= '';
		$POSPANTAU			= '';
		
		if($criteria=='harvesting_ticket'){
			/*$SQL01 = 'SELECT COUNT(T0."SOL_SPTA") AS "CHECKPOINTTOTAL" FROM '.$this->a_table.'."SOL_POS" T0 
				LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 
				ON T1."CODE"=T0."SOL_POSPANTAU"
				WHERE T1."SOL_STATUS"=\'1\' AND T1."SOL_ISEMPLACEMENT"=\'N\' 
				AND T0."SOL_SPTA" ='.$findme;*/
			$SQL01 = 'SELECT COUNT(T0."SOL_SPTA") AS "CHECKPOINTTOTAL" FROM '.$this->a_table.'."SOL_POS" T0 
				LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 
				ON T1."CODE"=T0."SOL_POSPANTAU"
				WHERE T1."SOL_ISEMPLACEMENT"=\'N\' 
				AND T0."SOL_SPTA" ='.$findme;	
		
			$CHECKPOINTCHECK 	= $this->rundataquery($SQL01); 
			
			$CHECKPOINTCOUNT	= isset($CHECKPOINTCHECK[0]['CHECKPOINTTOTAL'])
				?$CHECKPOINTCHECK[0]['CHECKPOINTTOTAL'] 
				:0;
				
			/*$SQL02 = 'SELECT COUNT(T0."SOL_SPTA") AS "PARKINGPOINTTOTAL" FROM '.$this->a_table.'."SOL_POS" T0 
				LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 
				ON T1."CODE"=T0."SOL_POSPANTAU"
				WHERE T1."SOL_STATUS"=\'1\' AND T1."SOL_ISEMPLACEMENT"=\'Y\' 
				AND T0."SOL_SPTA"='.$findme;*/
			
			$SQL02 = 'SELECT COUNT(T0."SOL_SPTA") AS "PARKINGPOINTTOTAL" FROM '.$this->a_table.'."SOL_POS" T0 
				LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 
				ON T1."CODE"=T0."SOL_POSPANTAU"
				WHERE T1."SOL_ISEMPLACEMENT"=\'Y\' 
				AND T0."SOL_SPTA"='.$findme;	
			
			$PARKINGAREACHECK = $this->rundataquery($SQL02); 
			
			$PARKINGAREACOUNT	= isset($PARKINGAREACHECK[0]['PARKINGPOINTTOTAL'])
				?$PARKINGAREACHECK[0]['PARKINGPOINTTOTAL'] 
				:0;
			
			
		}
		
		if(($CHECKPOINTCOUNT > 0 && $PARKINGAREACOUNT > 0) OR ($CHECKPOINTCOUNT > 0 && $PARKINGAREACOUNT <= 0)){
			#di pos pantau ada di emplasemen ada 
			#$WCOND 		= 'T6."SOL_ISEMPLACEMENT"=\'N\' AND T6."SOL_STATUS"=1 AND ';
			$WCOND 		= 'T6."SOL_ISEMPLACEMENT"=\'N\' AND ';
			$POSPANTAU	= ',T6."PRINTEDCODE" AS "pos_pantau"';
		}elseif($CHECKPOINTCOUNT <= 0 && $PARKINGAREACOUNT > 0){
			#di pos pantau ada parking area tidak ada 
			#$WCOND 		= 'T6."SOL_ISEMPLACEMENT"=\'Y\' AND T6."SOL_STATUS"=1 AND ';
			$WCOND 		= 'T6."SOL_ISEMPLACEMENT"=\'Y\' AND ';
			$POSPANTAU 	=',\'-\' AS "pos_pantau"';
		}else{
			$WCOND 		= '';
			$POSPANTAU 	=',\'-\' AS "pos_pantau"';
		}
		
		return ['wcond' => $WCOND,'pospantau' => $POSPANTAU];
	}
	/*
	 * validate emplasemen 
	 * @ckpcode	: checkpointcode 
	 */
	public function validateparkingarea($ckpcode){
		$checkus	= 'N';
		
		$SQL = 'SELECT T0."CODE"
		,T0."NAME"
		,T0."SOL_ISEMPLACEMENT" 
		FROM '.$this->a_table.'."SOL_POSPANTAU" T0 WHERE T0."CODE"=\''.$ckpcode.'\'';
		#echo $SQL;die;
		$recs		= $this->rundataquery($SQL); 
		
		if(count($recs) > 0){
			/*echo '<pre>';
			print_r($recs); 
			echo '</pre>';
			die();*/
			$checkus	= $recs[0]['SOL_ISEMPLACEMENT'];
		}
		
		return $checkus;
	}
	
	/*
	 * GET NEXT NOMOR ANTRIAN 
	 */
	public function getnextqueue($day){
		$SQL = 'SELECT * FROM '.$this->a_table.'."SOL_POS" T0
		WHERE T0."SOL_ANTRIAN" <> \'\' ORDER BY T0."DOCENTRY" DESC LIMIT 1'; 
		
		$recs		= $this->rundataquery($SQL); 
		
		return $recs;
	}

	/*
     * Blitar, 28 of April 2020 	INSERT SPTA 
	 * @dt : array data 
	 */	
	public function insertspta($dt){
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

	/*
	 * inserting to pos pantau (RMI_ADDON_MASTER."SOL_POS")
	 * params : spta, harvesting ticket 
	 *			type, IN our OUT 
	 *			check_point, id of check point 
	 */	
	#public function savecheckpoint($spta,$type,$check_point){
	public function savecheckpoint($spta,$type,$check_point,$longitude='',$latitude='',$location=''){
		//added on 28 of May 2020 
		$spta			= strtoupper($spta);
		$type			= strtoupper($type);
		$check_point	= strtoupper($check_point);
		//--------
		//05 of May 2020
		$spta	= str_replace('2-','',$spta); 
		$checktrans	= $this->checkpointtranscheck($spta,$check_point); 
		/*echo '<pre>';
		print_r($checktrans); 
		echo '</pre>';
		die; */
		if(count($checktrans) <= 0){
			//if in parking area make in
			$parkingcheck = $this->validateparkingarea($check_point);
			
			if($parkingcheck=='Y'){
				$type='IN';
			}else{
				$type='OUT';
			}
			#echo $longitude.'--'.$latitude.'<br>'; 
			//--------------
			#$SQL 	= "CALL ".$this->a_table.".RMI_INSERT_POS ("."'".$spta."','".$type."','".$check_point."'".")";
			#$SQL 	= "CALL ".$this->a_table.".RMI_INSERT_POSV2 ("."'".$spta."','".$type."','".$check_point."','".$longitude."','".$latitude."','".$location."'".")";
			$SQL 	= "CALL ".$this->a_table.".RMI_INSERT_POSV2 ("."'".$spta."','".$type."','".$check_point."','".$longitude."','".$latitude."',''".")";
			#echo $SQL;die;
			$dt 	= $this->rundataquery($SQL); 
			$ret	= 'no-ok';	
			if(count($dt) > 0){
				if(isset($dt[0]['\'Ok\''])){
					$check = $dt[0]['\'Ok\''];
					if($check=='Ok'){
						$ret = 'ok';
					}
				}
			}
		}else{
			$ret = 'ok';
		}
		return $ret;
	}
	/*
	 * function to check whether SPTA has been inserted 
	 * 07/05/2020 	
	 * parameter, harvesting_ticket (spta) 
	 */
	private function checkpointtranscheck($harvesting_ticket,$check_point){
		$SQL = 'SELECT * FROM '.$this->a_table.'."SOL_POS" T0 
		WHERE T0."SOL_SPTA"=\''.$harvesting_ticket.'\' AND T0."SOL_POSPANTAU"=\''.$check_point.'\'
		ORDER BY T0."DOCENTRY" DESC LIMIT 1';
		#die($SQL);
		$dt 	= $this->rundataquery($SQL); 
		return $dt;
	} 

	/*
	 * Get nomor antrian 
	 */	
	public function getinsertedqueuenumber($spta){
		$ret = [];
		
		//--QUERY LAMA--
		/*$SQL = 'SELECT T0."DOCENTRY",T0."SOL_SPTA",T0."SOL_ANTRIAN"
		,T0."SOL_ANTRIAN",T1."NAME",T0."SOL_POSPANTAU" 
		FROM '.$this->a_table.'."SOL_POS" T0
		LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 
		ON T1."CODE"=T0."SOL_POSPANTAU"
		WHERE T0."SOL_SPTA"=\''.$spta.'\' AND T0."SOL_ANTRIAN"!=\'\' ORDER BY T0."DOCENTRY" DESC LIMIT 1';*/
		
		$SQL = 'SELECT * FROM (SELECT ROW_NUMBER() OVER (PARTITION BY YEAR(T0."SOL_DATE") ORDER BY T0."DOCENTRY") AS "TRUCK_KE"
		,T0."DOCENTRY",T0."SOL_SPTA",T0."SOL_ANTRIAN",T1."NAME",T0."SOL_POSPANTAU",T0."SOL_DATE",T1."SOL_ISEMPLACEMENT" 
		,T2."SOL_NOPOL"
		FROM '.$this->a_table.'."SOL_POS" T0
		LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1
		ON T1."CODE"=T0."SOL_POSPANTAU"
		LEFT JOIN '.$this->a_table.'."SOL_SPTA" T2 ON T2."DOCNUM"=T0."SOL_SPTA"
		WHERE YEAR(T0."SOL_DATE")=YEAR(CURRENT_DATE) AND T1."SOL_ISEMPLACEMENT"=\'Y\'
		ORDER BY T0."DOCENTRY" ASC) T0
		WHERE T0."SOL_SPTA"=\''.$spta.'\'';
		
		#echo $SQL;die;
		
		$dt 	= $this->rundataquery($SQL); 
		//altered on 21 of May 2020 
		/*if(count($dt) > 0){
			$ret	= $dt['0']['SOL_ANTRIAN'];
		}
		return $ret;*/
		if(count($dt) > 0){
			$ret	= $dt[0];
		}
		return $ret;		
	}

	/*
	 * check apakah masih dapat nomor antrian 
	 * 27/05/2020
	 */	
	public function checkfivetythousandquota(){
		$ret = []; 
		
		$SQL = 'SELECT * FROM 
		(SELECT ROW_NUMBER() OVER (PARTITION BY YEAR(T0."SOL_DATE") ORDER BY T0."DOCENTRY" DESC) AS "KUOTA"
		,T0."DOCENTRY",T0."SOL_SPTA",T0."SOL_ANTRIAN",T1."NAME",T0."SOL_POSPANTAU",T0."SOL_DATE"
		,T1."SOL_ISEMPLACEMENT",T2."SOL_STATUS" 
		FROM '.$this->a_table.'."SOL_POS" T0
		LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1
		ON T1."CODE"=T0."SOL_POSPANTAU"
		LEFT JOIN '.$this->a_table.'."SOL_SPTA" T2 ON T2."DOCNUM"=T0."SOL_SPTA"
		WHERE YEAR(T0."SOL_DATE")=YEAR(CURRENT_DATE) AND T1."SOL_ISEMPLACEMENT"=\'Y\' 
		AND T2."SOL_STATUS"!=\'L\'
		ORDER BY T0."DOCENTRY" ASC) 
		T0
		WHERE 
		YEAR(T0."SOL_DATE")=YEAR(CURRENT_DATE) LIMIT 1';
		
		#echo $SQL;
		#die('_SKIPED_');
		$dt 	= $this->rundataquery($SQL); 
		if(count($dt) > 0){
			$ret	= $dt[0]['KUOTA'];
		}
		return $ret;	
	}
	
	public function getFarmer($findme,$criteria,$day)
	{
		$ret = []; 
		
		if($findme && $criteria && $day){
			//--added on 05/05/2020--
			//for ommiting 2- in spta number 
			if($criteria=='harvesting_ticket'){
				$findme	= str_replace('2-','',$findme); 
				$sptadate	= substr(date('Y'),0,2).substr($findme,0,6);
				//$expiration	= date('Ymd',strtotime('+1 day', strtotime($sptadate)));
			}
			//-----------------------
			$wcon			= $this->getwconforexiststpa($findme,$criteria);
			$myyear			= substr($day,0,4);
		
			/*$SQL = 'SELECT 
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
				,\'\' AS "harvesting_ticket"
				,\'\' AS "tgl_spta"
				,\'\' AS "driver"
				,\'\' AS "police_number"
				,\'\' AS "vehicle_type"
				,\'N\' AS "RMI_ISVALID"
				,\'N\' AS "RMI_TYPE"
				,\'\' AS "RMI_AREAID"
				,\'\' AS "RMI_PROJECTID"
				,\'\' AS "RMI_BRIXSCG"
				,\'0000-00-00 00:00:00\' AS "RMI_ESTTEBANG"
				,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,\'0\' AS "brix"
				,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")=\''.$myyear.'\') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\' AND T21."SOL_QUANTITY" > 0) AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
				WHERE YEAR(T1."SOL_TO")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 
				AND T2."SOL_QUANTITY" > 0
				'.$wcon.'  ORDER BY T3."DOCENTRY" DESC LIMIT 1';*/
				$SQL = 'SELECT 
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
				,\'\' AS "harvesting_ticket"
				,\'\' AS "tgl_spta"
				,\'\' AS "driver"
				,\'\' AS "police_number"
				,\'\' AS "vehicle_type"
				,\'N\' AS "RMI_ISVALID"
				,\'N\' AS "RMI_TYPE"
				,\'\' AS "RMI_AREAID"
				,\'\' AS "RMI_PROJECTID"
				,\'\' AS "RMI_BRIXSCG"
				,\'0000-00-00 00:00:00\' AS "RMI_ESTTEBANG"
				,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
				,\'0\' AS "brix"
				,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"
				FROM '.$this->a_table.'."SOL_PETANI" T0 
				LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
				FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_TO")=\''.$myyear.'\') AS T1
				ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
				ON T2."DOCENTRY"=T1."DOCENTRY" 
				LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
				LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
				WHERE YEAR(T1."SOL_TO")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
				AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
				AND T1."ROW_NUM"=1 '.$wcon.'  ORDER BY T3."DOCENTRY" DESC LIMIT 1';	
			
			$recs 	= $this->rundataquery($SQL);
			;
			if(count($recs) > 0){
				if(!empty($recs[0]['driver']) && !empty($recs[0]['police_number']) && !empty($recs[0]['brix'])){
					$recs[0]['show_button'] = 1;
				}else{
					$recs[0]['show_button'] = 0;
				}
						
				//$recs[0]['images']	= ['brix'=>'','truck'=>''];
				$recs[0]['images']	= ['brix'=>'','truck'=>'','vregister'=>''
									 ,'brix_scg_photo' => '','truck_scg_photo' => ''
									];			
			}
		}
		
		return $recs;
	}
	
	/*
	 * 04 Juli 2020 17:00 (GMT+7)
	 * Checking Farmer Quota
	 *	FN_CHECKQUOTA(nomor register,baseentry,baseline,tanggalSPTA)
	 *	
	 *	jika return -99 <- berarti jadwal tebang belum di input
	 *	jika return 0 <- quota sudah habis
	 *	jika return >0  <- sisa quota	 
	 */
	
	public function callQuota($params)
	{
		$SQL='SELECT '.$this->a_table.'.FN_CHECKQUOTA(\''.$params['register'].'\','.
			$params['baseentry'].','.$params['baseline'].',\''.
			$params['date'].'\') AS "quota" FROM DUMMY';
		#echo $SQL;die;
		$query=$this->rundataquery($SQL);
		$out=$query[0]['quota'];
		
		return (int)$out;
	}
	
	/*
	 * Added 14/07/2020 08:11
	 * showsptadata alt
	 *
	 */
	
	public function showsptadata_alt($findme,$criteria,$day,$checkpoint){
		$cparr=['00019','00020','00021'];
		
		if(in_array($checkpoint,$cparr)){
			$expiration = date('Ymd',strtotime('+1 day', strtotime($day)));
			$recs		= [];
			if($findme && $criteria && $day){
				//--added on 05/05/2020--
				//for ommiting 2- in spta number 
				if($criteria=='harvesting_ticket'){
					$findme		= str_replace('2-','',$findme); 
					$sptadate	= substr(date('Y'),0,2).substr($findme,0,6);
					$expiration	= date('Ymd',strtotime('+1 day', strtotime($sptadate)));
					
				}
				//-----------------------
				$wcon			= $this->getwconforexiststpa($findme,$criteria);
				$myyear			= substr($day,0,4);
				//---added on 19 Mei 2020 
				$insentifcheck 	= $this->insentifcheck($findme,$criteria); 
				$pospantaufield	= $insentifcheck['pospantau'];
				$pospantaucond	= $insentifcheck['wcond'];
				
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
						,T3."SOL_KENDARAAN" AS "vehicle_type",T3."TRUCK_STATUS"
						,T7."Name" AS "jenis_truck"
						,T6."RMI_FLAG"
						,TO_DECIMAL(IFNULL(T3."SOL_RENDEMEN",0),10,0) AS "rendemen"
						,TO_DECIMAL(IFNULL(T3."SOL_BRIX",0),10,0) AS "brix"
						,CASE IFNULL(T8."SOL_TIPE",\'\') WHEN  \'1\' THEN \'TRS\' WHEN  \'2\' THEN \'TRT\' ELSE \'-\' END AS "TipeKebun"
						,TO_DECIMAL(IFNULL(T3."SOL_POL",0),10,0) AS "pol"'.$pospantaufield.'
						FROM '.$this->a_table.'."SOL_PETANI" T0 
						LEFT JOIN (SELECT *,ROW_NUMBER() OVER (PARTITION BY T20."SOL_REGISTRASI" ORDER BY T20."DOCENTRY" DESC) AS "ROW_NUM" 
						FROM '.$this->a_table.'."SOL_KONTRAK_H" T20 WHERE T20."SOL_STATUS"=\'O\' AND YEAR(T20."SOL_FROM")=\''.$myyear.'\') AS T1
						ON T1."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
						LEFT JOIN (SELECT * FROM '.$this->a_table.'."SOL_KONTRAK_D" T21 WHERE T21."SOL_STATUS"=\'O\') AS T2
						ON T2."DOCENTRY"=T1."DOCENTRY" 
						LEFT JOIN '.$this->a_table.'."SOL_SPTA" T3 ON T3."SOL_REGISTRASI"=T0."SOL_REGISTRASI"
						LEFT JOIN '.$this->a_table.'."SOL_WB" T4 ON T4."U_SOL_SPTA"=T3."DOCNUM" AND T4."U_SOL_STATUS"!=\'R\'
						LEFT JOIN '.$this->a_table.'."SOL_POS" T5 ON T5."SOL_SPTA"=T3."DOCNUM" 
						LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T6 ON T6."CODE"=T5."SOL_POSPANTAU" 
						LEFT JOIN '.$this->c_table.'."@SOL_JENISKENDARAAN" T7 ON CAST(T3."SOL_KENDARAAN" AS NVARCHAR(50)) = CAST(T7."Code" AS NVARCHAR(50))
						LEFT JOIN '.$this->a_table.'."SOL_KEBUN" T8 ON T8."SOL_REGISTRASI" = T0."SOL_REGISTRASI"
						WHERE '.$pospantaucond.'YEAR(T1."SOL_FROM")=\''.$myyear.'\' AND T1."SOL_STATUS"=\'O\' 
						AND T0."SOL_CARDCODEREFNAME" NOT LIKE \'Dummy%\'
						AND T1."ROW_NUM"=1 
						AND YEAR(T5."SOL_DATE")=YEAR(CURRENT_DATE)'.
						' AND T3.SOL_STATUS IN (\'A\',\'I\') '.
						' AND TO_DATE(T3.SOL_APPROVE_TIMESTAMP) BETWEEN TO_DATE(\''.date('Y-m-d H:i:s',(int)microtime(true)).'\') AND TO_DATE(\''.date('Y-m-d 06:00:00',strtotime(date('Y-m-d')."+1 days")).'\')'.
						$wcon.' ORDER BY T3."DOCENTRY" DESC LIMIT 1';
				#die('_AHA_');
				#echo $SQL;die('_SKIP_');
				$recs		=	$this->rundataquery($SQL); 
				#echo $SQL;die('_SKIP_');
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
		}else{
			$recs=$this->showsptadata($findme,$criteria,$day);
		}
		
		return $recs;
	}
	
	public function checkLimitPetani($findme,$criteria)
	{
		#die('CALLDED');
		$out='';
		$crarr=['farmer_id','contract_id','identity_card'];
		
		if(in_array($criteria,$crarr))
		{
			if($findme && $criteria){
				if($criteria=='farmer_id'){
					$wcond='WHERE T0."SOL_REGISTRASI"=\''.$findme['farmer_id'].'\'';
					$SQL='SELECT
						T0."SOL_REGISTRASI",
						IFNULL(RMI_ADDON_MASTER.rmi_hitungtotalregister(t0."SOL_REGISTRASI"),0) AS "TotalRegister"
						FROM RMI_ADDON_MASTER."SOL_PETANI" T0 '.$wcond;
				}
				
				if($criteria=='contract_id'){
					$wcond='WHERE T0."SOL_NOKONTRAK"=\''.$findme['contract_id'].'\'';
					$SQL='SELECT
						T0."SOL_REGISTRASI",
						IFNULL(RMI_ADDON_MASTER.rmi_hitungtotalregister(t0."SOL_REGISTRASI"),0) AS "TotalRegister"
						FROM RMI_ADDON_MASTER."SOL_KONTRAK_H" T0 '.$wcond;
				}
				
				if($criteria=='identity_card'){
					$wcond='WHERE T0."SOL_KTP"=\''.$findme['identity_card'].'\'';
					$SQL='SELECT
						T0."SOL_REGISTRASI",
						IFNULL(RMI_ADDON_MASTER.rmi_hitungtotalregister(t0."SOL_REGISTRASI"),0) AS "TotalRegister"
						FROM RMI_ADDON_MASTER."SOL_PETANI" T0 '.$wcond;
				}
				#echo $SQL.'<br>';	
				$recs=$this->rundataquery($SQL); 
				/*echo '<pre>';
				print_r($recs); 
				echo '</pre>';
				die(); */
				if($recs[0]['TotalRegister']>=4000000000)
				{
					$out=[
						'criteriapass'=>true,
						'limitpass'=>false,
						'recs'=>$findme,
						'checkpass'=>'limit',
					];
				}else{
					$out=[
						'criteriapass'=>true,
						'limitpass'=>true,
						'recs'=>$findme,
						'checkpass'=>'clear',
					];
				}
			}else{
				$out=[
					'criteriapass'=>true,
					'limitpass'=>false,
					'recs'=>$findme,
				];
			}
			
		}else{
			$out=[
				'criteriapass'=>false,
				'limitpass'=>false,
				'recs'=>$findme,
			];
		}
		
		return $out;
	}
	
	/*
	 * check ritase use (untuk block kode petak)
	 * 17 06 2022
	 */
	/*
	 * check ritase use (untuk block kode petak)
	 */
	public function checkritaseuse($area_id){
		$area_id	= strtoupper($area_id);
		$projectype = trim(substr($area_id,0,1)); 
		$areaid     = trim(substr($area_id,1));
		$arrproject = ['M','D','H','B','V','P'];
		$recs 		= []; 
		$ret 		= ['sts'=>'nok', 'message' => 'plot id tidak dikenali']; 
		if(in_array($projectype,$arrproject)){
			$SQL 		= 'SELECT RMI_ADDON_MASTER.COUNTRITASE(IFNULL(AREAID,\'\')
			,IFNULL(PROJECTID,0),IFNULL(PROJECTYPE,\'\')) AS "TOTALSPTAAREAID"
			,* FROM "RMI_ADDON_MASTER"."RMI_MST_PROJECT" 
			WHERE AREAID=\''.$areaid.'\' AND PROJECTYPE=\''.$projectype.'\'';
			/*echo '<pre>';
			print_r($SQL); 
			echo '</pre>';
			die;*/
			$recs = $this->rundataquery($SQL);
		} 
		
		
		if(count($recs) > 0){
			$dt 	= $recs[0]; 
			$ritase 	= $dt['RITASE'];
			$usedritase = $dt['RITASEUSED'];
			$totalspta 	= $dt['TOTALSPTAAREAID'];
			#$totalspta = 70;
			if($totalspta < $ritase){
				$ret 		= ['sts'=>'ok'
							,'message' => 'Jatah ritase '.number_format($ritase,0,'.',',').', Total SPTA '.$totalspta
							]; 
			}else{
				$ret 		= ['sts'=>'nok'
							,'message' => 'Jatah ritase '.number_format($ritase,0,'.',',').', Total SPTA '.$totalspta
							]; 
			}	
		}
		
		return $ret;
	}
	/*
	 * Check tonase if total weigh out more than estimated kg, give 
	 * warning
	 * */
	public function checktonage($area_id){
		$area_id	= strtoupper($area_id);
		$projectype = trim(substr($area_id,0,1)); 
		$areaid     = trim(substr($area_id,1));
		$arrproject = ['M','D','H','B','V','P'];
		$ret 		= ['sts'=>'nok', 'message' => 'plot id tidak dikenali']; 
		$recs 		= []; 
		if(in_array($projectype,$arrproject)){
			$SQL = 'SELECT rmp.AREAID,rmp.PROJECTYPE
				,rmp."ESTIMATEDKG"/1000 AS "ESTIMATEDKG"
				,IFNULL(SUM(((sw."U_SOL_QTY_WEIGH1"-sw."U_SOL_QTY_WEIGH2"))/1000),0) AS "NETTOKG"
				FROM RMI_ADDON_MASTER.RMI_MST_PROJECT rmp 
				LEFT JOIN RMI_ADDON_MASTER.SOL_SPTA ss 
				ON ss.RMI_AREAID=rmp.AREAID 
				LEFT JOIN RMI_ADDON_MASTER.SOL_WB sw 
				ON sw.U_SOL_SPTA=ss.DOCNUM 
				WHERE rmp.AREAID=\''.$areaid.'\' AND rmp.PROJECTYPE=\''.$projectype.'\'
				GROUP BY rmp.AREAID,rmp.PROJECTYPE
				,rmp.ESTIMATEDKG';
 			$recs = $this->rundataquery($SQL);		
			
		}	
		
		if(count($recs) > 0){
			$dt 	= $recs[0]; 
			$ESTIMATEDKG 	= $dt['ESTIMATEDKG'];
			$NETTOKG 		= $dt['NETTOKG'];
			
			if($NETTOKG < $ESTIMATEDKG){
				$ret 		= ['sts'=>'ok'
							,'message' => 'ESTIMASI TONASE '.number_format(($ESTIMATEDKG),0,'.',',').', TOTAL TONASE TIMBANG KELUAR '.number_format(($NETTOKG),0,'.',',')
							]; 
			}else{
				$ret 		= ['sts'=>'nok'
							,'message' => 'ESTIMASI TONASE '.number_format(($ESTIMATEDKG),0,'.',',').', TOTAL TONASE TIMBANG KELUAR '.number_format(($NETTOKG),0,'.',',')
							]; 
			}
		}	
		/*echo '<pre>';
		print_r($SQL);
		print_r($ret);
		print_r($dt);
		echo '</pre>';
		die;*/
		return $ret; 
	}
	
	/**
	 * check check point active status 
	 */
	public function getcheckpointstatus($checkpoint){
		if($checkpoint!='00020'){
			$SQL = 'SELECT RMI_ADDON_MASTER.POSPANTAU_STATUS(\''.$checkpoint.'\') 
			AS "CHECKPOINT_STS" FROM DUMMY';

			$recs = $this->rundataquery($SQL);

			$ret = 0; 
			if(count($recs) > 0){
				$ret = number_format($recs[0]['CHECKPOINT_STS'],0,'.',',');
			}
		}else{
			$ret = 1;

		}
		
		return $ret; 
	} 


	/**
    * added on 27 of August 2022 
    */
	public function getcheckpointquota($code){

		$ret = 0; 
		
		$SQL = 'SELECT T0."CODE",T0."QUOTA" FROM RMI_ADDON_MASTER."SOL_POSPANTAU" T0 
		WHERE T0."CODE"=\''.$code.'\'';
		#echo $SQL;die;
		$recs = $this->rundataquery($SQL);

		if(count($recs) > 0){
			$ret = isset($recs[0]['QUOTA'])?$recs[0]['QUOTA']:0;
		}

		return $ret; 
	}

	public function getusedcheckpointquota($code){

		$ret = 0; 

		$SQL = 'SELECT COUNT(*) AS "JML" 
		FROM RMI_ADDON_MASTER."SOL_SPTA" T0 
		WHERE T0."SOL_CREATEDATE"=CURRENT_DATE 
		AND T0."SOL_STATUS"!=\'L\' AND T0."SOL_SOURCE"=\''.$code.'\'';

		$recs = $this->rundataquery($SQL);

		if(count($recs) > 0){
			$ret = isset($recs[0]['JML'])?$recs[0]['JML']:0; 
		}

		return $ret; 
	}
	/**
	 * added on 30 of May 2023
	 * quota check point 5 rit per jam per pos pantau 
	 */
	public function getusedcheckpointquotav2($code){

		$ret = 0; 

		$SQL = 'SELECT COUNT(*) AS "JML" 
		FROM RMI_ADDON_MASTER."SOL_SPTA" T0 
		WHERE to_nvarchar(sol_approve_timestamp,\'yyyymmddHH24\')  =  to_nvarchar(now(),\'yyyymmddHH24\')
		AND T0."SOL_STATUS"!=\'L\' AND T0."SOL_SOURCE"=\''.$code.'\'';

		$recs = $this->rundataquery($SQL);

		if(count($recs) > 0){
			$ret = isset($recs[0]['JML'])?$recs[0]['JML']:0; 
		}

		return $ret; 
	}


 }
