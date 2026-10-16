<?php
/*
 * Blitar, 15 04 2020 
 * Controller Description : handle data syncronization from qr code, it will return SPTA number, Quee number, delivery ticket information  						
 */
namespace Zircon\Modules\Checkpointservices\Controllers;
use ReallySimpleJWT\Token;
use Zircon\Modules\Checkpointservices\Models\Syncronizedev AS Syncronize; 
//added untuk penyesuaian 
use Zircon\Modules\Checkpointservices\Models\Mysqldatacheck;
use Zircon\Modules\Checkpointservices\Models\Projectsetting; 
use Zircon\Modules\Checkpointservices\Models\Queue; 

class SyncronizedatadevController extends ControllerBase
{
	private $projectdata;
	private $projectcheck;
	private $validproject; 
	
    public function indexAction()
    {
		#die('OK');
		//added on 04 of August 2022 
		$syncmodel 		= new Syncronize();
		$check_point 	= $this->request->getPost('check_point');
		$icheckpointsts  = $syncmodel->getcheckpointstatus($check_point);
		#if($check_point=='00020'){
			if($icheckpointsts > 0){
				$tokensts=$this->tokenCheck();
				if($tokensts){
					//modified on 05 April 2022 
					//read project setting based on mysql data 
					$findme			= $this->request->getPost('findme');
					$criteria		= $this->request->getPost('criteria');
					$projectsetting = new Projectsetting(); 
					$this->projectdata 	= $projectsetting->getprojectsetting();
					/*echo '<pre>';
					print_r($this->projectdata);
					echo '</pre>';
					die('_SKIP_');*/
					$this->projectcheck = 1; 
					if($criteria=='field_id'){
						$findme 	= strtoupper($findme);
						$checkus 	= substr($findme,0,1);
						if(!isset($this->projectdata[$checkus])){
							$this->validproject = [];
							$this->projectcheck = 0; 
						}else{
							$this->validproject = $this->projectdata[$checkus];
							$this->projectcheck = 1;
						}
					}
					#die($checkus.'---'.$this->projectcheck);
					//----------
					if($this->projectcheck == 1){
						//altered on 17 of June 2022 
						if($this->projectcheck== 1 && $criteria=='field_id'){
							$syncmodel 		= new Syncronize();
							//call checkritaseuse
							$ritasecheck 	= $syncmodel->checkritaseuse($findme);
							$rsts 			= isset($ritasecheck['sts'])
											?$ritasecheck['sts']
											:'';
							$rmsg 			= isset($ritasecheck['message'])
											?$ritasecheck['message']
											:'';
							if($rsts=='ok'){
								$day			= date('Ymd');
								#$day			= '20200418';
								$appresponse	= $this->checkRequestStandResponseRet($day);
								$status			= $appresponse['status'];
								$messagedt		= $appresponse['messagedt'];
							}else{
								#die('_OKE_');
								$status		= ['code' => '401 ','sts' => 'warning'];
								$messagedt	= ['message' => $rmsg];
							}				
						}else{
							$day			= date('Ymd');
							#$day			= '20200418';
							$appresponse	= $this->checkRequestStandResponseRet($day);
							$status			= $appresponse['status'];
							$messagedt		= $appresponse['messagedt'];
						}
						
					}else{
						#die('_SKIP_');
						//show warning when project is still not activated 
						$status		= ['code' => '401 ','sts' => 'warning'];
						$messagedt	= ['message' => 'Project belum aktif'];
					}
				}else{
					$status			= ['code' => '401 ','sts' => 'Unauthorized'];
					$messagedt		= ['message' => 'No token supplied or token is not valid'];
				}
			}else{
				$status			= ['code' => '401 ','sts' => 'Pos pantau tidak aktif'];
				$messagedt		= ['message' => 'Pos pantau tidak aktif'];
			}
		/*}else{
			$status			= ['code' => '402 ','sts' => 'Unauthorized'];
			$messagedt		= ['message' => 'Pos pantau tidak aktif'];
		}*/

		

		$ret 		= $this->setJsonResponse($messagedt,$status);		
		$this->getJsonResponse($ret);
		//disabling view 
		$this->view->disable();
    }
	
	/*
	 * Checking mysql data (sugarcanegis) 
	 */
	private function checksugarcanegisdata(){
		
		$sqlcheck 		= new Mysqldatacheck();
		$findme			= $this->request->getPost('findme');
		$criteria		= $this->request->getPost('criteria');
		//deteksi nilai brix_scg 
		$brix_scg     = $this->request->getPost('brix_scg');
		
		$ret 			= array();
		
		if($criteria=='field_id'){
			$findme = strtoupper($findme);
			
			if(substr($findme,0,1)=='D'){	
				$SQL=$this->getDroneID($findme);
				#die($SQL);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($findme,0,1)=='H'){	
				$SQL=$this->getTHijauID($findme);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($findme,0,1)=='M'){	
				$SQL=$this->getTManisID($findme);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($findme,0,1)=='B'){	
				$SQL=$this->getTBibitID($findme);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($findme,0,1)=='V'){	
				$SQL=$this->getTVarietasID($findme);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($findme,0,1)=='P'){	
				$SQL=$this->getTPupukID($findme);
				$recs = $sqlcheck->rundataquery($SQL);
			}
			/*else{
				$SQL	=$this->getDroneID($findme);
				$crecs 	= $sqlcheck->rundataquery($SQL);
				
				if(count($crecs) <= 0){
					$SQL=$this->getTHijauID($findme);
					$crecs = $sqlcheck->rundataquery($SQL);					
				}
				$recs = $crecs;
			}*/
			/*echo '<pre>';
			print_r($SQL);
			echo '</pre>';
			die('_SKIP_');*/ 
			if(count($recs) > 0){
					$ret = $recs[0];
					$ret = ['KTP_ID'		 => $recs[0]['ktp_id']
					        ,'NAME'			 => $recs[0]['name']
					        ,'RMI_ISVALID' 	 => 'N'
					        ,'RMI_TYPE'    	 => strtoupper(trim(substr($recs[0]['project'],0,1)))
						    ,'RMI_AREAID'  	 => $recs[0]['field_id']
							,'RMI_PROJECTID' => $recs[0]['project_id']
							,'RMI_BRIXSCG' 	 => 0
							,'PROJECT'		 => $recs[0]['project']
							,'RMI_ESTTEBANG' => isset($recs[0]['estimated_harvesting_day'])
												?date('Y-m-d',strtotime($recs[0]['estimated_harvesting_day']))
												:'0000-00-00'
						];
					if($brix_scg){
						$ret['RMI_BRIXSCG'] = $brix_scg;
					}		
				}								
		}
		
		return $ret;
	}  
	
	/*
	 * Request check and response 
	 * Blitar, 21 of April 2020  
	 */
	private function checkRequestStandResponseRet($day){
		#die($day);
		//----post variable------
		$findme			= $this->request->getPost('findme');
		$findme			= strtoupper($findme);
		$criteria		= $this->request->getPost('criteria');
		$parking_area	= $this->request->getPost('parking_area');
		$check_point	= $this->request->getPost('check_point');
		#$arrcriteria	= ['farmer_id','contract_id','identity_card'];
		$arrcriteria	= ['farmer_id','contract_id','identity_card','field_id'];
		
		//scan dengan menggunakan field_id (kode petak mapper)--- 
		$gisdata = $this->checksugarcanegisdata();
		
		/*echo '<pre>'; 
		print_r($gisdata); 
		echo '</pre>';
		die('_SKIP_'.__LINE__);*/
		
		#echo '<pre>';
		if(count($gisdata) > 0){
			$findme	 = $gisdata['KTP_ID'];
			$criteria	= 'identity_card';
			#print_r($gisdata); 
		}
		
		//---------------------------
		$findme	= str_replace('2-','',$findme);
		//-----------------------
		$syncmodel 		= new Syncronize();  
		//-----------------------
		$status		= ['code' => '404','sts' => 'default message'];
		$messagedt	= ['data' =>[]];
		#$this->getFarmerQuota($findme,$criteria);
		if(!$parking_area){
			
			if(in_array($criteria,$arrcriteria)){
				#die('_OKE_');
				//---checking queuelist (07 of June 2021)---
				#$queuecheck = $this->checkinqueuelist($findme,$criteria,$day);
				$queuecheck = 'allowed';
				if($queuecheck=='allowed'){
					//added on 14 of May 2025
					$checkbp	= $syncmodel->validatefarmerv2($findme,$criteria,$day); 
					#die($checkbp.'---');
					//-----------------------
					if($checkbp=='valid-data'){
						//------------------------
						#die('_EXECUTED_');
						//--check validity of farmer based on contract_id,farmer_id,or identity_card
						$farmercheck	= $syncmodel->validatefarmer($findme,$criteria,$day,true);
						#die($farmercheck.'#'.$checkbp.'--'.$findme.'#'.$criteria);
						if($farmercheck=='valid'){
							//condition when farmer data is valid 
							//not on parking area, display existing SPAT or create new one when it's not available 
							//this condition also apply for truck that come to parking area with no harvesting ticket 
							$recs		= $syncmodel->checkexistingspta($findme,$criteria,$day,$check_point);
							#$recs		= array();
							if(count($recs) > 0){
								//return tampilkan 
								$status		= ['code' => '200','sts' => 'Success'];
								$messagedt	= ['data' => $recs];	
								//insert to SOL_POS 
							}else{
								//if no harvesting ticket create new one first 
								$farmerdt	= $syncmodel->validatefarmer($findme,$criteria,$day,false);
								if(count($farmerdt) > 0){
									
									//do insert here (use transaction feature)
									$recs=$syncmodel->getFarmer($findme,$criteria,$day);
									
									/*
									$recs =	$syncmodel->insertspta($newspta);
									*/
									//$recsTes		= $syncmodel->checkfarmerData($findme,$criteria,$day,$check_point);
									if(count($recs) > 0){
										/*echo '<pre>';
										print_r($criteria);
										print_r($recs); 
										echo '</pre>';
										die('_AHA2_');*/
										/*
										* check limit 4M petani
										*/
										$checkLimit=$this->checkLimitPetani($recs[0],$criteria);
										#$checkLimit['status']=='clear';
										// checkpass limit bila status clear, baiklah jalan, manakala bentur limit status 405 tertayang layar status
										#die($checkLimit['status']);
										if($checkLimit['status']=='clear'){
											#die('_EXECUTED_');
											$status		= ['code' => '200','sts' => 'Success'];
											$messagedt	= ['data' => $recs
											//$recs
											];
										}else if($checkLimit['status']=='limit'){
											
											$status		= ['code' => '405','sts' => 'Petani terblokir karena mencapai limit, mohon hubungi petugas'];	
										}
									}else{
										//SPTA tidak bisa diinsert 
										$status		= ['code' => '412','sts' => 'Error insert  SPTA'];
										$messagedt	= ['data' =>[]];
									}						
									//kasih ret sama dengan yang di atas 
								}else{
									$status		= ['code' => '412','sts' => 'Data petani belum terdaftar atau tidak valid, mohon hubungi petugas'];
									$messagedt	= ['data' =>[]];
								}
								
								#$status		= ['code' => '404','sts' => 'on progress create dulu spta, return akan sama dengan yang sudah ada SPTA'];
								#$messagedt	= ['data' =>[]];
							}
						}else{
							//farmer data not valid 
							$status		= ['code' => '412','sts' => 'Data petani belum terdaftar atau tidak valid, mohon hubungi petugas'];
							$messagedt	= ['data' =>[]];
						}
					}else{
						//else here 
						if($checkbp=='dummy-bp'){
							$status		= ['code' => '412','sts' => 'Rekening petani belum diset'];
							$messagedt	= ['data' =>[]];
						}elseif($checkbp=='no-contract'){
							$status		= ['code' => '412','sts' => 'Kontrak giling belum diajukan'];
							$messagedt	= ['data' =>[]];
						}else{
							$status		= ['code' => '412','sts' => 'Data petani belum terdaftar atau tidak valid, mohon hubungi petugas'];
							$messagedt	= ['data' =>[]];
						}
					}					
				}else{
					//ndak masuk daftar antrian 
					$status		= ['code' => '412','sts' => 'Belum masuk daftar antrian'];
					$messagedt	= ['data' =>[]];
				}
			}else{
				//harvesting ticket scan on check point station 
				$recs		= $syncmodel->showsptadata($findme,$criteria,$day);
				
				if(count($recs) > 0){
					$status		= ['code' => '200','sts' => 'Success'];
					$messagedt	= ['data' => $recs];
				}else{
					$status		= ['code' => '412','sts' => 'SPTA belum tercetak untuk petani ini, mohon scan nomor kontrak, register, atau KTP'];
					$messagedt	= ['data' =>[]];
				}				
			}			
			#die('_OKE_');
		}else{
			//check if in parking area, print out queue ticket 
			if($parking_area && $criteria=='harvesting_ticket' && $check_point){
				$parkingcheck	= $syncmodel->validateparkingarea($check_point);
				if($parkingcheck=='Y'){
					$recs			= $syncmodel->showsptadata($findme,$criteria,$day);
					
					if(count($recs) > 0){
						$status		= ['code' => '200','sts' => 'Success'];
						$messagedt	= ['data' => $recs];
					}else{
						//SPTA belum ada 
						$status		= ['code' => '404','sts' => 'SPTA belum terdaftar, mohon scan nomor kontrak, register atau KTP, centang create SPTA'];
						$messagedt	= ['data' =>[]];
					}
				}else{
					//parking area just print queue ticket 
					$status		= ['code' => '404','sts' => 'Scanning tidak dilakukan di emplasemen'];
					$messagedt	= ['data' =>[]];
				}				
			}else{
				if($criteria=='police_number'){
					//$recs			= $syncmodel->showsptadata($findme,$criteria,$day);
					$recs			= $syncmodel->showsptadata_alt($findme,$criteria,$day,$check_point);
					
					if(count($recs) > 0){
						$status		= ['code' => '200','sts' => 'Success'];
						$messagedt	= ['data' => $recs];
					}else{
						//SPTA belum ada 
						$status		= ['code' => '404','sts' => 'SPTA belum terdaftar, untuk nomor polisi ini'];
						$messagedt	= ['data' =>[]];
					}
				}else{
					//parking area just print queue ticket 
					$status		= ['code' => '404','sts' => 'Scan hanya SPTA di emplasemen'];
					$messagedt	= ['data' =>[]];
				}
			}
		}
		
		/*echo '<pre>';
		print_r(['status' => $status,'messagedt' => $messagedt]); 
		echo '</pre>';
		die('_OE_');*/
		
		return ['status' => $status,'messagedt' => $messagedt];
	}
	
	/*
	 * prepare data untuk insert ke SOL_SPTA
	 * Blitar, 21 of April 2020 
	 */
	private function preparesptadata($farmerdt,$day,$check_point){
		$ret	= []; 
		if(count($farmerdt) > 0){
			$syncmodel 		= new Syncronize();
			foreach($farmerdt as $k => $v){
				$ret['DOCNUM']			= $syncmodel->getNextSptaNumber($day);
				$ret['SOL_REGISTRASI']	= $v['SOL_REGISTRASI'];
				$ret['BASEENTRY']		= $v['BASEENTRY'];
				$ret['BASELINE']		= $v['BASELINE'];
				$ret['SOL_QUANTITY']	= 7000;
				$ret['SOL_STATUS']		= 'A';
				$ret['SOL_NOPOL']		= '';
				$ret['SOL_SUPIR']		= '';
				$ret['SOL_KENDARAAN']	= '';
				$ret['SOL_DATE']		= $day;
				$ret['SOL_EXPIRY']		= date('Ymd', strtotime("+1 day", strtotime($day)));
				$ret['SOL_CREATEDATE']	= date('Ymd');
				$ret['SOL_SOURCE']		= $check_point; 
				$ret['SOL_APPROVE_TIMESTAMP']	= date('YmdHis');
			}
		}
		return $ret;
	}
	
	/*
	 * token check 
	 */
	private function tokenCheck(){
		$stscheck	= '';
		if($this->request->isPost()){
			$token		= $this->request->getPost('token');
			if($token){
				$stscheck 	= Token::validate($token, $this->config->tsecret);
			}	
		}		
		return $stscheck;
	}
	
	private function setJsonResponse($dt,$status){	
		$tmpret=['status'=>$status,'data'=>$dt];
		$ret=json_encode($tmpret);
	
		return $ret;
	}
	
	private function getJsonResponse($ret){
		header("Content-disposition: attachment; filename=gettokenresp.json;charset=utf-8");
		header('Content-type: application/json');
		echo $ret;
	}
	
	// Farmer Quota Function
	/*	
	protected function getFarmerQuota($findme,$criteria,$check_point)
	{
		$sync=new Syncronize();
		$arrcriteria	= ['farmer_id','contract_id','identity_card'];
		
		if(in_array($criteria,$arrcriteria)){
			$day=date('Ymd');
			$farmercheck	= $syncmodel->validatefarmer($findme,$criteria,$day,true);
			
			if($farmercheck=='valid'){
				$farmerdata	= $syncmodel->validatefarmer($findme,$criteria,$day,false);
				
				$checker=$this->preparesptadata($farmerdata,$day,$check_point);
				
				$params=[
					'register'=>$checker['SOL_REGISTRASI'],
					'baseentry'=>$checker['BASEENTRY'],
					'baseline'=>$checker['BASELINE'],
				];
				
				$quota=$syncmodel->callQuota($params);
				
				echo $quota;
			}
			
			exit;
		}
		//if(quota>jatah_tebang){
		// 		json response
		//exit
		//}
		
	}
	*/
	/**/
	private function checkLimitPetani($findme,$criteria)
	{
		#die('II');
		$sync=new Syncronize();
		$limit=$sync->checkLimitPetani($findme,$criteria);
			
		if($limit['limitpass'])
		{
			return ['recs'=>$limit['recs'],'status'=>$limit['checkpass']];
		}else{
			return ['recs'=>$limit['recs'],'status'=>$limit['checkpass']];
		}
	}
	
	public function clAction()
	{
		$syncmodel=new Syncronize();
		
		$findme			= $this->request->getPost('findme');
		$criteria		= $this->request->getPost('criteria');
		
		$day=date('Ymd');
		$recs=$syncmodel->getFarmer($findme,$criteria,$day);
		
		//echo "<pre>";
		//print_r($recs);
		//echo "</pre>";
		$return=$this->checkLimitPetani($recs[0],$criteria);
		echo "<pre>";
		print_r($return);
		echo "</pre>";
		$return=json_encode($return);
		
		header("Content-disposition: attachment; filename=gettokenresp.json;charset=utf-8");
		header('Content-type: application/json');
		
		echo $return;
	}
	//*/
	/*
	 * Check data queue (07 Juni 2021) -check in queue list-
	 */
	private function checkinqueuelist($findme,$criteria,$day){
		$ret 	= 'not-allowed';
		
		$findme	= str_replace('2-','',$findme);
		//-----------------------
		$syncmodel 		= new Syncronize();  
		$queue			= new Queue();
		$arrcriteria	= ['farmer_id','contract_id','identity_card','field_id'];
		if(in_array($criteria,$arrcriteria)){
			$recs			= $syncmodel->getFarmer($findme,$criteria,$day);
			$identity_card	= isset($recs[0]['identity_card'])?$recs[0]['identity_card']:'';
			if($identity_card){
				$queue = new Queue();
				$SQL   = 'SELECT QUEUEID,SETID,VEHICLEID,REGISTERID,POSID FROM TRX_QUEUE 
				WHERE REGISTERID=\''.$identity_card.'\'';
				$qrecs = $queue->gettabledata($SQL);
				
				if($qrecs){
					$qnumber 	= isset($qrecs[0]['setid'])?$qrecs[0]['setid']:'';
					if($qnumber){
						#die($qnumber);
						//allowed untuk masuk 
						$ret = $this->getcurrentroundsetqueue($qnumber); 
					}
				}				
			}
		}		
		return $ret;
	}
	/*
	 * get current round set and allowed queue 
	 */
	private function getcurrentroundsetqueue($qnumber){
		$retarr 	= array(); 
		$roundset 	= '';
		$SQL 	= "SELECT *,CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', 1),INTEGER) AS FROMA
		,CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', -1),INTEGER) AS TOA
		FROM QUEUE_ROUNDSET 
		WHERE CURRENT_DATE() BETWEEN roundset_dstart AND roundset_dend AND
		".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', 1),INTEGER) AND
		CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', -1),INTEGER) 
		 OR
		 ".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_fuso, '-', 1),INTEGER) AND
		CONVERT(SUBSTRING_INDEX(roundset_fuso, '-', -1),INTEGER)
		OR
		".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_double, '-', 1),INTEGER) AND
		CONVERT(SUBSTRING_INDEX(roundset_double, '-', -1),INTEGER)";
		#echo $SQL;die;
		#$SQL 	= 'SELECT * FROM QUEUE_ROUNDSET 
		#WHERE CURRENT_DATE() BETWEEN roundset_dstart AND roundset_dend';
		
		#$CHOUR	= date('H'); 
		
		$queue 	= new Queue();
		$recs 	= $queue->gettabledata($SQL); 
		
		if(count($recs) > 0){
			/*echo '<pre>';
			print_r($recs); 
			echo '</pre>';
			die;*/
			$roundset = $recs[0]['roundset'];			
		}
		#die($roundset); 
		if(empty($roundset)){
			//revisi lagi untuk jam 00 dan 01
			if($CHOUR=='00' || $CHOUR=='01' || $CHOUR=='02'){
				/*$SQL2 = 'SELECT *,DATE_SUB(CURRENT_DATE(), INTERVAL 2 DAY) FROM QUEUE_ROUNDSET 
				WHERE DATE_SUB(CURDATE(), INTERVAL 2 DAY) BETWEEN roundset_dstart AND roundset_dend
				AND roundset=12';*/
				
				$SQL 	= "SELECT *,CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', 1),INTEGER) AS FROMA
				,CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', -1),INTEGER) AS TOA
				FROM QUEUE_ROUNDSET 
				WHERE DATE_SUB(CURDATE(), INTERVAL 1DAY) BETWEEN roundset_dstart AND roundset_dend AND
				".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', 1),INTEGER) AND
				CONVERT(SUBSTRING_INDEX(roundset_engkel, '-', -1),INTEGER) 
				OR
				 ".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_fuso, '-', 1),INTEGER) AND
				CONVERT(SUBSTRING_INDEX(roundset_fuso, '-', -1),INTEGER)
				OR
				".$qnumber." BETWEEN CONVERT(SUBSTRING_INDEX(roundset_double, '-', 1),INTEGER) AND
				CONVERT(SUBSTRING_INDEX(roundset_double, '-', -1),INTEGER)";
				
				$queue 	= new Queue();
				$recs2 	= $queue->gettabledata($SQL2); 
				
				if(count($recs2) > 0){
					$roundset = $recs2[0]['roundset'];					
				}
			}
		}
		
		$ret = $this->checkroundset($roundset);
		
		return $ret;		
	}
	/*
	 * checking MST_SETQUEUE
	 */	
	private function checkroundset($roundset){
		$ret = 'not-allowed'; 
		$SQL = 'SELECT * FROM MST_SETQUEUE WHERE SETID=\''.$roundset.'\'';
		$queue 	= new Queue();
		$recs 	= $queue->gettabledata($SQL);
		if(count($recs) > 0){
			$dlimit = date('H:i:s',strtotime($recs[0]['timefrom'])+21600);
			if(strtotime(date('H:i:s')) < strtotime($dlimit)){
				$ret = 'allowed';
			}
		}	
		return $ret;	
	}
	
	
	private function getDroneID($param=null)
	{
		$m_cutting_days		= 0; 
		if(isset($this->validproject['minimum_cutting_days'])){
			$m_cutting_days = $this->validproject['minimum_cutting_days']; 
		}
		
		$param = str_replace('D','',$param);
		
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME,T3.KTP_ID,T2.VILLAGE
			,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE ,T0.COORDINATE
			,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES ,T1.SOIL_TYPE 
			,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY,T1.PLANTING_HISTORY 
			,T1.SUGAR_CANE_CLASS,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date",T0.CREATED
			,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id"
			,CASE WHEN T0.ID > 0 THEN \'DRONE\' ELSE \'REGULER\' END AS "PROJECT"
			, T4.ESTIMATED_RITASE, T4.SERVICE_PRICE,T4.TONAGE
			,T5.ID AS "project_id",T4.SPRAY_DATE,DATE_ADD(T4.SPRAY_DATE,INTERVAL '.$m_cutting_days.' DAY) AS "ESTIMATED_HARVESTING_DAY"
			FROM DRONE_PROJECT 
			T0 LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			LEFT JOIN DRONE_PROJECT_DETAIL T4 ON T4.BaseID=T0.ID
			LEFT JOIN FERT_ISSUE T5 ON T5.DRONEDET_ID=T4.ID
			WHERE CURRENT_DATE >= DATE_ADD(T4.SPRAY_DATE,INTERVAL '.$m_cutting_days.' DAY) AND T5.ID IS NOT NULL AND T1.ID=\''.$param.'\'';		
		#die($SQL);	
		return $SQL; 	
	}
	
	private function getTHijauID($param=null)
	{
		$param = str_replace('H','',$param);
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME
			,T3.KTP_ID,T2.VILLAGE,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE 
			,T0.COORDINATE,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES 
			,T1.SOIL_TYPE ,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY
			,T1.PLANTING_HISTORY ,T1.SUGAR_CANE_CLASS
			,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date"
			,T0.CREATED,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id" 
			,CASE WHEN T0.ID > 0 THEN \'HIJAU\' ELSE \'REGULER\' END AS "PROJECT" 
			,T0.ESTIMATED_RITASE,T0.ESTIMATED_TONAGE
			,T0.ID AS "project_id",T0.ESTIMATED_HARVESTING_DAY 
			FROM TEBU_HIJAU_PROJECT T0 
			LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			WHERE T1.CRU_PERIOD='.date('Y').' AND farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		return $SQL;	
	}
	
	private function getTManisID($param=null)
	{
		$param = str_replace('M','',$param);
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME
			,T3.KTP_ID,T2.VILLAGE,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE 
			,T0.COORDINATE,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES 
			,T1.SOIL_TYPE ,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY
			,T1.PLANTING_HISTORY ,T1.SUGAR_CANE_CLASS
			,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date"
			,T0.CREATED,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id" 
			,CASE WHEN T0.ID > 0 THEN \'MANIS\' ELSE \'REGULER\' END AS "PROJECT" 
			,T0.ESTIMATED_RITASE,T0.ESTIMATED_TONAGE
			,T0.ID AS "project_id",T0.ESTIMATED_HARVESTING_DAY 
			FROM TEBU_MANIS_PROJECT T0 
			LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			WHERE T1.CRU_PERIOD='.date('Y').' AND farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		return $SQL;	
	}
	
	private function getTBibitID($param=null)
	{
		$param = str_replace('B','',$param);
		
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME
			,T3.KTP_ID,T2.VILLAGE,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE 
			,T0.COORDINATE,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES 
			,T1.SOIL_TYPE ,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY
			,T1.PLANTING_HISTORY ,T1.SUGAR_CANE_CLASS
			,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date"
			,T0.CREATED,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id" 
			,CASE WHEN T0.ID > 0 THEN \'BIBIT\' ELSE \'REGULER\' END AS "PROJECT" 
			,T0.ESTIMATED_RITASE,T0.ESTIMATED_TONAGE
			,T0.ID AS "project_id",T0.ESTIMATED_HARVESTING_DAY 
			FROM TEBU_BIBIT_PROJECT T0 
			LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			WHERE T1.CRU_PERIOD='.date('Y').' AND farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		return $SQL;	
	}
	/**
	 * added on 18 April 2023 
	 */
	private function getTVarietasID($param=null)
	{
		$param = str_replace('V','',$param);
		
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME
			,T3.KTP_ID,T2.VILLAGE,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE 
			,T0.COORDINATE,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES 
			,T1.SOIL_TYPE ,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY
			,T1.PLANTING_HISTORY ,T1.SUGAR_CANE_CLASS
			,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date"
			,T0.CREATED,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id" 
			,CASE WHEN T0.ID > 0 THEN \'VARIETAS\' ELSE \'REGULER\' END AS "PROJECT" 
			,T0.ESTIMATED_RITASE,T0.ESTIMATED_TONAGE
			,T0.ID AS "project_id",T0.ESTIMATED_HARVESTING_DAY 
			FROM TEBU_VARIETAS_PROJECT T0 
			LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			WHERE T1.CRU_PERIOD='.date('Y').' AND farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		return $SQL;	
	}

	private function getTPupukID($param=null)
	{
		$param = str_replace('P','',$param);
		
		$SQL = 'SELECT T3.ID as "farmer_id",T3.NAME
			,T3.KTP_ID,T2.VILLAGE,T2.SUB_DISTRIC,T2.DISTRICTS,T2.PROVINCE 
			,T0.COORDINATE,T1.SUGAR_CANE_CATEGORY,T1.SUGAR_CANE_VARIETIES 
			,T1.SOIL_TYPE ,T1.WATER_SOURCES,T1.DISTANCE_TO_FACTORY
			,T1.PLANTING_HISTORY ,T1.SUGAR_CANE_CLASS
			,T1.SURFACE_AREA ,T1.FIELD_ID,T3.SUBZONE
			,T0.CREATED_BY ,T0.CREATED AS "survey_date"
			,T0.CREATED,T0.ID AS "AREA_ID",T0.AREA_ID AS "petak_id" 
			,CASE WHEN T0.ID > 0 THEN \'PUPUK\' ELSE \'REGULER\' END AS "PROJECT" 
			,T0.ESTIMATED_RITASE,T0.ESTIMATED_TONAGE
			,T0.ID AS "project_id"
			FROM TEBU_VARIETAS_PROJECT T0 
			LEFT JOIN FARMER_LAND_AREA T1 ON T1.ID = T0.AREA_ID 
			LEFT JOIN FARMER_LAND_LOCATION T2 ON T2.ID = T1.LAND_ID 
			LEFT JOIN FARMERS T3 ON T3.ID=T2.FARMER_ID 
			LEFT JOIN PUPUK_PROJECT_ISSUE ppi ON ppi.PUPUK_PROJECT_ID = T0.ID
			WHERE T1.CRU_PERIOD='.date('Y').' AND farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		return $SQL;	
	}

/*
class SyncronizedataController extends ControllerBase
{

    public function indexAction()
    {
		$tokensts=$this->tokenCheck();
		if($tokensts){
			$day			= date('Ymd');
			#$day			= '20200418';
			$appresponse	= $this->checkRequestStandResponseRet($day);
			$status			= $appresponse['status'];
			$messagedt		= $appresponse['messagedt'];
		}else{
			$status=['code' => '401 ','sts' => 'Unauthorized'];
			$messagedt=['message' => 'No token supplied or token is not valid'];
		}
		
		$ret 		= $this->setJsonResponse($messagedt,$status);		
		$this->getJsonResponse($ret);
		//disabling view 
		$this->view->disable();
    }
	
	/*
	 * Request check and response 
	 * Blitar, 21 of April 2020  
	
	private function checkRequestStandResponseRet($day){
		//----post variable------
		$findme			= strtoupper($this->request->getPost('findme'));
		$criteria		= $this->request->getPost('criteria');
		$parking_area	= $this->request->getPost('parking_area');
		$check_point	= $this->request->getPost('check_point');
		$arrcriteria	= ['farmer_id','contract_id','identity_card'];
		/*echo '<pre>';
		print_r($_POST);
		echo '</pre>';
		$findme	= str_replace('2-','',$findme);
		//-----------------------
		$syncmodel 		= new Syncronize();  
		//-----------------------
		$status		= ['code' => '404','sts' => 'default message'];
		$messagedt	= ['data' =>[]];
		
		#$this->getFarmerQuota($findme,$criteria,$check_point);
		
		if(!$parking_area){
			if(in_array($criteria,$arrcriteria)){
				//--check validity of farmer based on contract_id,farmer_id,or identity_card
				$farmercheck	= $syncmodel->validatefarmer($findme,$criteria,$day,true);
				if($farmercheck=='valid'){
					//condition when farmer data is valid 
					//not on parking area, display existing SPAT or create new one when it's not available 
					//this condition also apply for truck that come to parking area with no harvesting ticket 
					$recs		= $syncmodel->checkexistingspta($findme,$criteria,$day,$check_point);
					#$recs		= array();
					if(count($recs) > 0){
						//return tampilkan 
						$status		= ['code' => '200','sts' => 'Success'];
						$messagedt	= ['data' => $recs];	
						//insert to SOL_POS 
					}else{
						//if no harvesting ticket create new one first 
						$farmerdt	= $syncmodel->validatefarmer($findme,$criteria,$day,false);
						
						if(count($farmerdt) > 0){
							//$newspta	= $this->preparesptadata($farmerdt,$day,$check_point);
							/*echo '<pre>';
							print_r($newspta); 
							echo '</pre>';
							die('_SKIP_');
							//do insert here (use transaction feature)
							$recs=$syncmodel->getFarmer($findme,$criteria,$day);
							/*
							$recs =	$syncmodel->insertspta($newspta);
							
							//$recsTes		= $syncmodel->checkfarmerData($findme,$criteria,$day,$check_point);
							if(count($recs) > 0){
								$status		= ['code' => '200','sts' => 'Success'];
								$messagedt	= ['data' => $recs
								//$recs
								];	
							}else{
								//SPTA tidak bisa diinsert 
								$status		= ['code' => '412','sts' => 'Error insert  SPTA'];
								$messagedt	= ['data' =>[]];
							}						
							//kasih ret sama dengan yang di atas 
						}else{
							$status		= ['code' => '412','sts' => 'Data petani belum terdaftar atau tidak valid, mohon hubungi petugas'];
							$messagedt	= ['data' =>[]];
						}
						
						#$status		= ['code' => '404','sts' => 'on progress create dulu spta, return akan sama dengan yang sudah ada SPTA'];
						#$messagedt	= ['data' =>[]];
					}
				}else{
					//farmer data not valid 
					$status		= ['code' => '412','sts' => 'Data petani belum terdaftar atau tidak valid, mohon hubungi petugas'];
					$messagedt	= ['data' =>[]];
				}
			}else{
				//harvesting ticket scan on check point station 
				$recs		= $syncmodel->showsptadata($findme,$criteria,$day);
				if(count($recs) > 0){
					$status		= ['code' => '200','sts' => 'Success'];
					$messagedt	= ['data' => $recs];
				}else{
					$status		= ['code' => '412','sts' => 'SPTA belum tercetak untuk petani ini, mohon scan nomor kontrak, register, atau KTP'];
					$messagedt	= ['data' =>[]];
				}				
			}			
		}else{
			//check if in parking area, print out queue ticket 
			if($parking_area && $criteria=='harvesting_ticket' && $check_point){
				$parkingcheck	= $syncmodel->validateparkingarea($check_point);
				if($parkingcheck=='Y'){
					$recs			= $syncmodel->showsptadata($findme,$criteria,$day);
					if(count($recs) > 0){
						$status		= ['code' => '200','sts' => 'Success'];
						$messagedt	= ['data' => $recs];
					}else{
						//SPTA belum ada 
						$status		= ['code' => '404','sts' => 'SPTA belum terdaftar, mohon scan nomor kontrak, register atau KTP, centang create SPTA'];
						$messagedt	= ['data' =>[]];
					}
				}else{
					//parking area just print queue ticket 
					$status		= ['code' => '404','sts' => 'Scanning tidak dilakukan di emplasemen'];
					$messagedt	= ['data' =>[]];
				}				
			}else{
				if($criteria=='police_number'){
					$recs			= $syncmodel->showsptadata_alt($findme,$criteria,$day,$check_point);
					if(count($recs) > 0){
						$status		= ['code' => '200','sts' => 'Success'];
						$messagedt	= ['data' => $recs];
					}else{
						//SPTA belum ada 
						$status		= ['code' => '404','sts' => 'SPTA belum terdaftar, untuk nomor polisi ini'];
						$messagedt	= ['data' =>[]];
					}
				}else{
					//parking area just print queue ticket 
					$status		= ['code' => '404','sts' => 'Scan hanya SPTA di emplasemen'];
					$messagedt	= ['data' =>[]];
				}
			}
		}
		
		return ['status' => $status,'messagedt' => $messagedt];
	}
	
	/*
	 * prepare data untuk insert ke SOL_SPTA
	 * Blitar, 21 of April 2020 
	
	private function preparesptadata($farmerdt,$day,$check_point){
		$ret	= []; 
		if(count($farmerdt) > 0){
			$syncmodel 		= new Syncronize();
			foreach($farmerdt as $k => $v){
				$ret['DOCNUM']			= $syncmodel->getNextSptaNumber($day);
				$ret['SOL_REGISTRASI']	= $v['SOL_REGISTRASI'];
				$ret['BASEENTRY']		= $v['BASEENTRY'];
				$ret['BASELINE']		= $v['BASELINE'];
				$ret['SOL_QUANTITY']	= 7000;
				$ret['SOL_STATUS']		= 'A';
				$ret['SOL_NOPOL']		= '';
				$ret['SOL_SUPIR']		= '';
				$ret['SOL_KENDARAAN']	= '';
				$ret['SOL_DATE']		= $day;
				$ret['SOL_EXPIRY']		= date('Ymd', strtotime("+1 day", strtotime($day)));
				$ret['SOL_CREATEDATE']	= date('Ymd');
				$ret['SOL_SOURCE']		= $check_point; 
				$ret['SOL_APPROVE_TIMESTAMP']	= date('YmdHis');
			}
		}
		return $ret;
	}
	
	/*
	 * token check 
	
	private function tokenCheck(){
		$stscheck	= '';
		if($this->request->isPost()){
			$token		= $this->request->getPost('token');
			if($token){
				$stscheck 	= Token::validate($token, $this->config->tsecret);
			}	
		}		
		return $stscheck;
	}
	
	private function setJsonResponse($dt,$status){	
		$tmpret=['status'=>$status,'data'=>$dt];
		$ret=json_encode($tmpret);
	
		return $ret;
	}
	
	private function getJsonResponse($ret){
		header("Content-disposition: attachment; filename=gettokenresp.json;charset=utf-8");
		header('Content-type: application/json');
		echo $ret;
	}
	
	public function testAction()
	{
		if($this->request->isPost()){
			//echo "orait";
			$findme			= $this->request->getPost('findme');
			$criteria		= $this->request->getPost('criteria');
			$check_point	= $this->request->getPost('check_point');
			
			$this->getFarmerQuota($findme,$criteria,$check_point);
		}else{
			echo "no";
		}
	}
	
	public function testcheckpointsplitterAction()
	{
		if($this->request->isPost()){
			//echo "orait";
			$findme			= strtoupper($this->request->getPost('findme'));
			$criteria		= $this->request->getPost('criteria');
			$check_point	= $this->request->getPost('check_point');
			$day			= date('Ymd');
			
			$sync=new Syncronize();
			$recs=$sync->showsptadata_alt($findme,$criteria,$day,$check_point);
			//$recs= $syncmodel->showsptadata($findme,$criteria,$day);
			//print_r($recs);exit;
			if(count($recs) > 0){
				$status		= ['code' => '200','sts' => 'Success'];
				$messagedt	= ['data' => $recs];
			}else{
				//SPTA belum ada 
				$status		= ['code' => '404','sts' => 'SPTA belum terdaftar, untuk nomor polisi ini'];
				$messagedt	= ['data' =>[]];
			}
			
			$msg=$this->setJsonResponse($messagedt,$status);		
			$this->getJsonResponse($msg);
		}else{
			echo "no";
		}
	}
	
	protected function getFarmerQuota($findme,$criteria,$check_point)
	{
		$sync=new Syncronize();
		$arrcriteria_a	= ['farmer_id','contract_id','identity_card'];
		$arrcriteria_b  = ['harvesting_ticket','police_number'];
		$day=date('Ymd');
		if(in_array($criteria,$arrcriteria_a)){
			
			$farmercheck	= $sync->validatefarmer($findme,$criteria,$day,true);
			
			if($farmercheck=='valid'){
				$farmerdata	= $sync->validatefarmer($findme,$criteria,$day,false);
				
				$checker=$this->preparesptadata($farmerdata,$day,$check_point);
				//HVT -->538l4k
				$params=[
					'register'=>$checker['SOL_REGISTRASI'],
					'baseentry'=>$checker['BASEENTRY'],
					'baseline'=>$checker['BASELINE'],
					'date'=>$day,
				];
				
				$quota=$sync->callQuota($params);
				
				if($quota==-99)
				{
					$status=['code' => '200','sts' => 'Waduww rika mesti urung temu Pak PL setempat kye. Deneng jatah tebang rika ora ana'];
					$messagedt=['message' => $quota];
					
					$msg=$this->setJsonResponse($messagedt,$status);		
					$this->getJsonResponse($msg);
					exit;
				}else{
					if($quota==0){
						$status=['code' => '200','sts' => 'Kuota rika entong, bapak\'e'];
						$messagedt=['message' => $quota];
						
						$msg=$this->setJsonResponse($messagedt,$status);
						$this->getJsonResponse($msg);
						exit;
					}
				}
			}
			
		}else if(in_array($criteria,$arrcriteria_b)){
			$out=$sync->showsptadata($findme,$criteria,$day);
			
			if(!empty($out)){
				$finder=$out[0];
				$farmerdata	= $sync->validatefarmer($finder['farmer_id'],'farmer_id',$day,false);				
				$checker=$this->preparesptadata($farmerdata,$day,$check_point);				
				//print_r($checker);				
				$params=[
					'register'=>$checker['SOL_REGISTRASI'],
					'baseentry'=>$checker['BASEENTRY'],
					'baseline'=>$checker['BASELINE'],
					'date'=>$day,
				];				
				$quota=$sync->callQuota($params);
				if($quota==-99)
				{
					$status=['code' => '200','sts' => 'Waduww rika mesti urung temu PL setempat kye. Deneng jatah tebang rika ora ana'];
					$messagedt=['message' => $quota];
					
					$msg=$this->setJsonResponse($messagedt,$status);		
					$this->getJsonResponse($msg);
					exit;
				}else{
					if($quota==0){
						$status=['code' => '200','sts' => 'Kuota rika entong, bapak\'e'];
						$messagedt=['message' => $quota];
						
						$msg=$this->setJsonResponse($messagedt,$status);
						$this->getJsonResponse($msg);
						exit;
					}
				}
			}else{
				$status=['code' => '200','sts' => 'Kuota rika entong, bapak\'e'];
				$messagedt=['message' => ''];
						
				$msg=$this->setJsonResponse($messagedt,$status);
				$this->getJsonResponse($msg);
				exit;
			}
			
			
		}
		//if(quota>jatah_tebang){
		// 		json response
		//exit
		//}
		
	}
	*/
}