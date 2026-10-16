<?php
namespace Zircon\Modules\Checkpointservices\Controllers;

use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser,Sptalog};
use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
use Zircon\Modules\Checkpointservices\Models\{AES,Spta,Syncronize};

//added untuk penyesuaian 
use Zircon\Modules\Checkpointservices\Models\Mysqldatacheck; 
use Zircon\Modules\Checkpointservices\Models\Projectsetting;

class UpdatesptaController extends ControllerBase
{
	private $projectdata;
	private $projectcheck; 
	private $validproject;
	private $reg_check_point_brix; 
	private $reg_empl_point_brix; 
	
	private $brx_warning; 
	private $overquota;
	#private $checkpoint_brix; 
	#private $emplasemen_brix;
	
	#private $check_point_brix; 
	#private $emplasemen_brix; 
	
	public function indexAction()
	{
		$this->brx_warning = 0; 
		$this->overquota   = 0;
		//die('Trouble');
		#phpinfo();
		$tokensts			= $this->apiHelper->tokenCheck();
		$this->reg_check_point_brix = 16; 
		$this->reg_empl_point_brix	= 16;
		
		try{
			if($tokensts){				
				if($this->request->isPost()){ 

                    $log_data = json_encode($this->request->getPost());
                    file_put_contents('/var/www/zircon/public/sptalog/'.$this->request->getPost('findme').'_'.date('ymdhis'),$log_data);    
                    /*echo '<pre>';
                    print_r($log_data);
                    echo '</pre>';
                    die('_SKIP_');*/

					$findme			= $this->request->getPost('findme');
					$findme			= str_replace('2-','',$findme);
					
					$criteria		= $this->request->getPost('criteria');
					$projectsetting 	= new Projectsetting(); 
					$this->projectdata 	= $projectsetting->getprojectsetting();
					
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
					/*echo '<pre>';
					print_r($this->projectdata).'<hr>';
					print_r($this->validproject); 
					echo '</pre>';
					die('_SKIP_LINE_'.__LINE__);*/
					if($this->projectcheck >=1){
						$checkpoint     = $this->request->getPost('checkpoint'); 
						//pengecekan brix kurang dari 18 (13 Juni 2021) 
						$brix_scg      	= $this->request->getPost('brix_scg');
						$brix          	= $this->request->getPost('brix');
						$findme			= $this->request->getPost('findme');
						$criteria		= $this->request->getPost('criteria');
						
						//Remark before 00:01 (03 of august 2021) 
						$checkproject = $this->checksugarcanegisdata();
						/*echo '<pre>';
						print_r($checkproject); 
						echo '</pre>';
						die('ON LINE : '.__LINE__);*/
						$arremplasemen 	= ['00020','00019'];
						$brixchecksts 	= 'ok';
						if(in_array($checkpoint,$arremplasemen)){
							if($brix_scg > 0){
								if(isset($checkproject['RMI_TYPE'])){
									//brix check based on project setting 
									$prj_prefix = $checkproject['RMI_TYPE']; 
									if(isset($this->projectdata[$prj_prefix])){
										$this->validproject = $this->projectdata[$prj_prefix];
									}
									
									$empl_brix = isset($this->validproject['project_prefix'])
												?$this->validproject['emplasemen_point_brix']
												:0;
									//remark sementara (27 Agustus 2022)			
									/*if($empl_brix > 0){
										if($brix_scg < $empl_brix){
											$brixchecksts = 'nok';
											$this->brx_warning = $empl_brix;
										}
									}else{*/
										//do reguler check 
										if($brix_scg < $this->reg_empl_point_brix){
											$brixchecksts = 'nok';
											$this->brx_warning = $this->reg_empl_point_brix;
										}	
									//}
									//end of remark 
									//remove this when previous line worked 
									/*if($checkproject['RMI_TYPE']=='D'){
										if($brix_scg < $empl_brix){
											$brixchecksts = 'nok';
										}	
									}elseif($checkproject['RMI_TYPE']=='H'){
										if($brix_scg < $empl_brix){
											$brixchecksts = 'nok';
										}
									}elseif($checkproject['RMI_TYPE']=='B'){
										if($brix_scg < $empl_brix){
											$brixchecksts = 'nok';
										}
									}elseif($checkproject['RMI_TYPE']=='M'){
										if($brix_scg < $empl_brix){
											$brixchecksts = 'nok';
										}
									}*/
								}else{
									//check for reguler check
									if($brix_scg < $this->reg_empl_point_brix){
										$brixchecksts = 'nok';
										$this->brx_warning = $this->reg_empl_point_brix;
									}	
								}
							}
							//emplasemen_brix zero & brix on check point more than zero
							if($brix_scg <=0 && $brix > 0){
								if($criteria=='field_id'){
									if(isset($checkproject['RMI_TYPE'])){
										$prj_prefix = $checkproject['RMI_TYPE']; 
										if(isset($this->projectdata[$prj_prefix])){
											$this->validproject = $this->projectdata[$prj_prefix];
										}
										$checkpoint_brix = isset($this->validproject['check_point_brix'])
										?$this->validproject['check_point_brix']
										:0;
										if($brix < $checkpoint_brix){
											$brixchecksts = 'nok';
										}else{
											if($brix < $this->reg_check_point_brix){
												$this->brx_warning = $this->reg_check_point_brix;
												$brixchecksts = 'nok';
											}
										}
										//remove this when previous line worked 
										/*if($checkproject['RMI_TYPE']=='D'){
											if($brix < 24){
												$brixchecksts = 'nok';
											}	
										}elseif($checkproject['RMI_TYPE']=='H'){
											if($brix < 23){
												$brixchecksts = 'nok';
											}
										}*/
									}	
								}else{
									if($brix < $this->reg_check_point_brix){
										$this->brx_warning = $this->reg_check_point_brix;
										$brixchecksts = 'nok';
									}
								}
							}
						}else{
							//continue here 
							if($criteria=='field_id'){
								/*if($brix < 22){
									$brixchecksts = 'nok';
								}*/
								if(isset($checkproject['RMI_TYPE'])){
									$prj_prefix = $checkproject['RMI_TYPE']; 
									if(isset($this->projectdata[$prj_prefix])){
										$this->validproject = $this->projectdata[$prj_prefix];
									}
									$checkpoint_brix = isset($this->validproject['check_point_brix'])
									?$this->validproject['check_point_brix']
									:0;
									if($brix < $checkpoint_brix){
										$brixchecksts = 'nok';
										$this->brx_warning = $checkpoint_brix;
									}
									/*if($checkproject['RMI_TYPE']=='D'){
										if($brix < 24){
											$brixchecksts = 'nok';
										}	
									}elseif($checkproject['RMI_TYPE']=='H'){
										
										if($brix < 23){
											$brixchecksts = 'nok';
										}
									}*/
								}								
							}else{
								if($brix < $this->reg_check_point_brix){
									$brixchecksts = 'nok';
									$this->brx_warning = $this->reg_check_point_brix;
								}
							}
						}					
						
						#added on 14 of August 2023 (preven brix input more than 30) 
						
						if($brix_scg > 0){
							if($brix_scg > 30){
								$brixchecksts = 'nok';
							}
						}
						
						if($brix > 0){
							if($brix > 30){
								$brixchecksts = 'nok';
							}
						}
						#die($brixchecksts);
						if($brixchecksts=='ok'){
							$vehicle_no		= strtoupper($this->request->getPost('vehicle_no'));
							#echo '<pre>';
							#print_r($gisdata); 
							#print_r($_POST);
							#echo '</pre>';
							#die('_AHA_');
							
							$sync = new Syncronize();
							$sync2= new Spta();
							if(in_array($checkpoint,$arremplasemen)){
								$checkvnspta = array(); 
							}else{
								$spta_num 		= $this->request->getPost('harvesting_ticket');
								$spta_num		= str_replace('2-','',$spta_num);
								$checkvnspta = $sync2->checksptanopol($vehicle_no,$spta_num);
							}
							
							if(count($checkvnspta) <= 0){
								$day=date('Ymd');
								$insertflag=FALSE;
								#echo $findme.'---'.$criteria.'<hr>';
								$checkpoint     = $this->request->getPost('checkpoint');
								
								$spta_num 		= $this->request->getPost('harvesting_ticket');
								$spta_num		= str_replace('2-','',$spta_num);
								
								$vehicle_no		= strtoupper($this->request->getPost('vehicle_no'));
								$driver_name 	= strtoupper($this->request->getPost('driver_name'));
								$vehicle_type	= $this->request->getPost('vehicle_type');
								$brix           = $this->request->getPost('brix');
								$longitude  	= $this->request->getPost('longitude');
								$latitude		= $this->request->getPost('latitude');
								#$location		= htmlspecialchars($this->request->getPost('location'));
								$location		= $this->request->getPost('location');
								$truck_status	= $this->request->getPost('truck_status');
								
								//deteksi nilai brix_scg 
								$brix_scg     = $this->request->getPost('brix_scg');
								
								$gisdata = $this->checksugarcanegisdata($spta_num);
								
								if(count($gisdata) > 0){
									if(!empty($gisdata['KTP_ID'])){
										$findme		= $gisdata['KTP_ID'];
										$criteria 	= 'identity_card';
									}
								}
								$farmerdt	= $sync->validatefarmer($findme,$criteria,$day,false);
								
								/*echo '<pre>';
								print_r($gisdata); 
								print_r($farmerdt); 
								echo '</pre>';
								die('_SKIP_');*/
								
								//print_r($sptaselection);
								//print_r($checkspta);
								$checkpoint=[
									'checkpoint'=>$checkpoint,
									'longitude'=>$longitude,
									'latitude'=>$latitude,
									'location'=>$location,
								];
								
								$mobspta=new Spta();
								if($spta_num=='')
								{
									#die('ex');
									//-- Tampered 14/07/2020 08:16
									//-- 		  28/07/2020 19:10
									$spta_num_pass2=$mobspta->checksptabydate($findme,$criteria,$vehicle_no,$driver_name,$day);
									
									/*echo '<pre>';
									print_r($spta_num_pass2); 
									echo '</pre>';
									die('_SKIP_');*/
									
									if($spta_num_pass2['spta_num']!=''){
										$spta_num=$spta_num_pass2['spta_num'];
										//tambah gis data
										$gisdata = $this->checksugarcanegisdata($spta_num);
										//---------------
										#$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$checkpoint,$spta_num,$brix);
										$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$checkpoint,$spta_num,$brix,$gisdata,$truck_status,'_LINE_293_');
										#die('_EXECUTED_');
										$insertflag=FALSE;							
									}elseif($criteria=='harvesting_ticket'){
										$spta_num = $findme;
										//tambahan untuk criteria harvesting ticket langsung update 
										$gisdata = $this->checksugarcanegisdata($spta_num);
										$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$checkpoint,$spta_num,$brix,$gisdata,$truck_status,'_LINE_300_');
										#die('+EEDD_');
										/*echo '<pre>';
										echo $spta_num.'<br>';
										print_r($gisdata); 
										echo '</pre>';*/
										
										$insertflag=FALSE;
									}
									else{
										/*echo '<pre>';
										print_r($farmerdt);
										echo '</pre>';
										die;*/
										$newspta = $this->preparenewsptadata($farmerdt,$vehicle_no,$driver_name,$vehicle_type,$brix,$day,$checkpoint,$truck_status);
										
										$syncdev    = new Syncronize(); 
										#$checkpoint
										$icheckpoint = $this->request->getPost('checkpoint');
										$checkpointquota = $syncdev->getcheckpointquota($icheckpoint);
										#$checkpointquota = 10;
										//if(){
										$usedquota       = $syncdev->getusedcheckpointquota($icheckpoint);
										//}else{
										//	$usedquota       = $syncdev->getusedcheckpointquotav2($icheckpoint);
										//}
										#die($usedquota.'--'.$checkpointquota);
											/*echo '<pre>';
											print_r($farmerdt);
											print_r($newspta);
											print_r($gisdata);
											echo '</pre>';
											die('_ON_LINE_'.__LINE__);*/
										if($usedquota < $checkpointquota){	
											if(count($gisdata) > 0){
												#echo 'OK<br>';
												$newspta['RMI_ISVALID']		= $gisdata['RMI_ISVALID'];
												$newspta['RMI_TYPE']		= $gisdata['RMI_TYPE'];
												$newspta['RMI_AREAID']		= $gisdata['RMI_AREAID'];
												$newspta['RMI_PROJECTID']	= $gisdata['RMI_PROJECTID'];
												if($brix_scg){
													$newspta['RMI_BRIXSCG']		= $brix_scg;	
												}else{
													$newspta['RMI_BRIXSCG']		= $gisdata['RMI_BRIXSCG'];	
												}								
												$newspta['RMI_ESTTEBANG']	= $gisdata['RMI_ESTTEBANG'];
												//check validasi tebu manis tanpa pos pantau 
												$arremplasemen = ['00020','00019'];
												if(in_array($checkpoint,$arremplasemen)){
													/*if($newspta['RMI_BRIXSCG'] >= 20){
														$newspta['RMI_ISVALID'] = 'Y';	
													}else{
														$newspta['RMI_ISVALID'] = 'N';												
													}*/
													$newspta['RMI_ISVALID'] = 'N';
													if(isset($gisdata['RMI_TYPE'])){
														$prj_prefix = $gisdata['RMI_TYPE'];
														if(isset($this->projectdata[$prj_prefix])){
															$this->validproject = $this->projectdata[$prj_prefix];
														}
														$empl_brix = isset($this->validproject['emplasemen_point_brix'])
														?$this->validproject['emplasemen_point_brix']
														:0;
														
														if($newspta['RMI_BRIXSCG'] >= $empl_brix){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
													}
													/*if($gisdata['RMI_TYPE']=='D'){
														if($newspta['RMI_BRIXSCG'] >= 24){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
													}elseif($gisdata['RMI_TYPE']=='H'){
														if($newspta['RMI_BRIXSCG'] >= 23){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
													}*/
												}else{
													/*if($brix >= 20 && $newspta['RMI_BRIXSCG'] >=20){
														$newspta['RMI_ISVALID'] = 'Y';
													}else{
														$newspta['RMI_ISVALID'] = 'N';		
													}*/
													$newspta['RMI_ISVALID'] = 'N';
													if(isset($gisdata['RMI_TYPE'])){
														$prj_prefix = $gisdata['RMI_TYPE'];
														if(isset($this->projectdata[$prj_prefix])){
															$this->validproject = $this->projectdata[$prj_prefix];
														}
														$checkpoint_brix = isset($this->validproject['check_point_brix'])
														?$this->validproject['check_point_brix']
														:0;
														
														$empl_brix = isset($this->validproject['emplasemen_point_brix'])
														?$this->validproject['emplasemen_point_brix']
														:0;
														
														if($brix >= $checkpoint_brix && $newspta['RMI_BRIXSCG'] >= $empl_brix){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
														
													}
													
													/*if($gisdata['RMI_TYPE']=='D'){
														if($brix >= 24 && $newspta['RMI_BRIXSCG'] >=24){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
													}elseif($gisdata['RMI_TYPE']=='H'){ 
														if($brix >= 23 && $newspta['RMI_BRIXSCG'] >=23){
															$newspta['RMI_ISVALID'] = 'Y';	
														}
													}*/
													
												}								
											}
											/*echo '<pre>';
											print_r($newspta); 
											echo '</pre>';
											die('_SKIP_');*/
											/*echo '<pre>';
											print_r($newspta); 
											echo '</pre>';
											die('_SKIP_'); */
											
											#die('O');
											$insspta = $sync2->pushspta($newspta);
											//continue here (overflow nomor SPTA) 
											/*echo '<hr>';
											echo "<pre>";
											print_r($insspta);
											print_r($newspta);
											echo "</pre>";
											die('_SKIP_'.__LINE__);*/
											if(count($insspta) > 0){
												/*echo '<pre>';
												print_r($insspta);
												echo '</pre>';
												die;*/
												$spta_num=(isset($insspta[0]['harvesting_ticket']))?$insspta[0]['harvesting_ticket']:$insspta[0]['harvesting_ticket'];
												$insertflag=TRUE;
											}
										}	
										else{
											//kasih penanda jika quota sudah over 
											$this->overquota = 1; 
										}	
										//end new condtion 
									}
									//-- 		      28/07/2020 19:10
									//-- End Tampered 14/07/2020 08:16
									
									/*
									$newspta = $this->preparenewsptadata($farmerdt,$vehicle_no,$driver_name,$vehicle_type,$brix,$day,$checkpoint);
									
									$insspta = $sync2->pushspta($newspta);
									*/
									/*echo "<pre>";
									print_r($insspta);
									echo "</pre>";
									die();*/
									/*
									$spta_num=$insspta[0]['harvesting_ticket'];
									$insertflag=TRUE;
									*/
								}else{	
									#die('UPDATED'); 
									$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$checkpoint,$spta_num,$brix,$gisdata,$truck_status,'_LINE_445_');
									$insertflag=FALSE;
								}
								
								
								
								$recs= $sync->showsptadata($spta_num,'harvesting_ticket',date('Ymd'));
								
								#echo '<pre>';
								#echo $spta_num.'<hr>';
								#print_r($gisdata); 
								#print_r($recs); 
								#echo '</pre>';
								#die;
								
								//die;
								if(count($recs)!=0 && count($recs)>0){
									$brixcheck = isset($recs[0]['images']['brix'])
										?$recs[0]['images']['brix']:'';
									$truckcheck	= isset($recs[0]['images']['truck'])
										?$recs[0]['images']['truck']:'';							
									
									$vregcheck	= isset($recs[0]['images']['vregister'])
										?$recs[0]['images']['vregister']:'';
										
									
									if($brixcheck==''&&$truckcheck==''&&$vregcheck==''){
									//if($brixcheck==''&&$truckcheck==''){
										$imagecheck=TRUE;
									}else{
										if($insertflag){
											$imagecheck=TRUE;
										}else{
											$imagecheck=FALSE;
										}
									}
									
									
									/*echo "<pre>";
									print_r($insertflag);
									echo "</pre>";
									die;*/
									//handle scg upload 
									$this->handlescgupload($spta_num);
									#$yearmod=date('Y');
									#$path_upload=$yearmod.'/'.$spta_num[2].$spta_num[3].'/'.$spta_num[4].$spta_num[5];
									#$dir='/var/www/test.rejosomanisindo.com/rmi_module/public/spta_upload/'.$path_upload;
									#echo '<pre>';
									#print_r($spta_num[2]);
									#echo '</pre>';
									#echo $dir;
									#die('_H_');
									//-----------------
									
									if($imagecheck){
										/*echo "<pre>";
										print_r($this->request->hasFiles());
										echo "</pre>";
										die;*/
										if($this->request->hasFiles()>1){
											$images     = $this->request->getUploadedFiles();
											
											/*echo '<pre>';
											print_r($images); 
											echo '</pre>';
											die('_SKIP_');*/
											$isUploaded = FALSE;
											$updateable=TRUE;
											$msglog='Ok. Uploading.';
										}else{
											$images='';	
											$updateable=FALSE;
											$msglog='No file(s) sent. You must choose at least one file to send. Please try again.';
										}							
									}else if(!$imagecheck){
										//tampered 14/07/2020 08:17 added: && $vregistercheck=''
										if($brixcheck=='' && $truckcheck=='' && $vregistercheck=''){
											$images='';							
											$msglog='No file(s) sent. You must choose at least one file to send. Please try again.';
											$updateable=FALSE;
										}else{
											$images='';
											$updateable=TRUE;
											$msglog='No file(s) are going to be uploaded. Image Path already exist.';
										}
									}
									
									if($images!=''){
										$load='ok';
										$yearmod=date('Y');
										$path_upload=$yearmod.'/'.$spta_num[2].$spta_num[3].'/'.$spta_num[4].$spta_num[5];
										$dir='/var/www/test.rejosomanisindo.com/rmi_module/public/spta_upload/'.$path_upload;
										#echo $dir;die('_OE_');
										if(!file_exists($dir) and !is_dir($dir))
										{				
											mkdir($dir,0775,true);							
										}
										
										$imgname=[];
										
										foreach($images as $key=>$image)
										{
											/*echo '<pre>';
											print_r($image); 
											echo '</pre>';
											die('_SKIP_');*/
											///Updated 14/07/2020
											//$filling=['brix','truck'];
											//remark for experiment 
											/*$filling=['brix','truck','vregister'];
											
											if(strpos($image->getname(),$filling[0])!==FALSE)
											{
												$newname=$filling[$key].'_'.$spta_num.'.'.$image->getExtension();
											}
												
											if(strpos($image->getname(),$filling[1])!==FALSE)
											{
												$newname=$filling[$key].'_'.$spta_num.'.'.$image->getExtension();
											}
											
											if(strpos($image->getname(),$filling[2])!==FALSE)
											{
												$newname=$filling[$key].'_'.$spta_num.'.'.$image->getExtension();
											}*/
											
											if(preg_match('/^brix/i',$image->getname())){
												$newname='brix_'.$spta_num.'.'.$image->getExtension();
											}
											
											if(preg_match('/^truck/i',$image->getname())){
												$newname='truck_'.$spta_num.'.'.$image->getExtension();
											}
											
											if(preg_match('/^vregister/i',$image->getname())){
												$newname='vregister_'.$spta_num.'.'.$image->getExtension();
											}
											
											$imgname[$key]=$newname;
											$ul=$dir.'/'.$imgname[$key];
											($image->moveTo($ul))?$isUploaded = true:$isUploaded = false;
											($isUploaded)?$msgimg[$key]=['m-'.$key=>$newname.' is uploaded.']:$msglog[$key]=['m-'.$key=>$newname.' some error occurred.'];
										}
										/*echo '<pre>';
										print_r($msgimg); 
										echo '</pre>';
										die('_SKIP_'); */
									}else{
										$load='no';
										$yearmod=date('Y');
										$path_upload=$yearmod.'/'.$spta_num[2].$spta_num[3].'/'.$spta_num[4].$spta_num[5];
										$dir='/var/www/test.rejosomanisindo.com/rmi_module/public/spta_upload/'.$path_upload;
										if(!file_exists($dir) and !is_dir($dir))
										{				
											mkdir($dir,0775,true);							
										}
									}
									
									if($updateable){
										$uppy='sure';
										$mobspta=new Spta();
										if($spta_num){
											$gisdata = $this->checksugarcanegisdata($spta_num);
										}
										$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$checkpoint,$spta_num,$brix,$gisdata,$truck_status,'_LINE_609_');
									}else{
										$uppy='no';
									}
									
									$arr = [
										'vehicle_no' => $vehicle_no,
										'driver_name' => $driver_name,
										'vehicle_type' => $vehicle_type,
										'spta_num' => $spta_num,
										'brix' => $brix,
									];
									/*echo '<pre>';
									print_r($arr); 
									echo '</pre>';
									die('BARIS : '.__LINE__);*/
									//print_r($this->checkUpdate($arr));
									#### for what ?
									if($this->checkUpdate($arr))
									{
										
										$checkpass='ok';
										
										$cptransaction = $sync->savecheckpoint($spta_num,'OUT',$checkpoint['checkpoint'],$longitude,$latitude,$location);
										
										//print_r($cptransaction);
										if($cptransaction=='ok'){
											
											$cpt='ok';
											
											$status=[
												'code'=>'200',
												'sts'=>'Success'
											];
											
											//alter by udin untuk return data SPTA 	
											$recs = $sync->showsptadata($spta_num,'harvesting_ticket',date('Ymd'));
											$messagedt	= [
												'data' => $recs,
												//'testdate'=>date('y-m-d'),
												'msg'=>$msglog,
												'imgstatus'=>(!empty($msgimg))?$msgimg:'',
											];
											
											/*
											$messagedt=['data' => 
												[
													'post'=>$_POST,
													'images'=>$images,
													'record'=>(count($recs)>0)?$recs[0]:'',
												],
												'msg'=>$msglog,
												'load'=>$load,
												'updateable'=>$uppy,
												'imgstatus'=>(!empty($msgimg))?$msgimg:'',
												'checkpass'=>$checkpass,
												'cpt'=>$cpt,
											];*/
										}else{
											$cpt='no';
											
											$status=[
												'code'=>'400',
												'sts'=>'Bad Request'
											];
											$messagedt=[
												'message'=>'The server could not understand the request due to invalid syntax.',
												'cpt'=>$cpt,
											];								
										}
									}else{
										
										#die('_OK_');
										$checkpass='no';
										
										$status=[
												'code'=>'412',
												'sts'=>'Precondition Failed'
											];
										$messagedt=[
											'message'=>'The client has indicated preconditions in its headers which the server does not meet.',
											'checkpass'=>$checkpass,
										];							
									}
								}else{
									#die('OK');
									if($this->overquota > 0){
										$status=[
											'code'=>'404',
											'sts'=>'Quota cetak pos pantau sudah melebihi batas'
										];
										$messagedt=['callback' => 'Quota cetak pos pantau sudah melebihi batas'];
									}else{
										#die('OOO');
										$status=[
											'code'=>'404',
											'sts'=>'Not Found'
										];
										$messagedt=['callback' => 'No record found.']; 
									}
								}
							}else{
								$status=[
									'code'=>'404',
									'sts'=>'warning'
								];
								$messagedt=['callback' => 'NOPOL BELUM TIMBANG KELUAR'];	
							}
						}else{
							$status=[
									'code'=>'404',
									'sts'=>'brix-warning'
								];
							if($this->brx_warning > 0){
								$messagedt=['callback' => 'brix kurang dari '.$this->brx_warning]; 
							}else{
								$messagedt=['callback' => 'brix tidak sesuai ketentuan.'];
							}
						}
					}else{
						$status=[
							'code'=>'404',
							'sts'=>'warning'
						];
						$messagedt=['callback' => 'project belum aktif']; 
					}
					//tambahan jika brix kurang dari 18 	
				}else{
					$status=[
							'code'=> '405',
							'sts'=>'Method Not Allowed'
							];
					$messagedt=['message'=>'Request method is known by the server but is not supported by the target resource.'];	
				}
				
			}else{
				$status=[
					'code'=>'401',
					'sts'=>'Unauthorized'
				];
				$messagedt=['message'=>'No token supplied or token is not valid'];
			}
			$ret=$this->apiHelper->setJsonResponse($messagedt,$status);
			$this->apiHelper->getJsonResponse($ret);
		}catch(Exception $e){
			//$sptalog=new Sptalog();
			
			
			$status=[
				'code'=>$e->code,
				'sts'=>$e->message,
			];
			$messagedt=['message'=>'Error'];
			$ret=$this->apiHelper->setJsonResponse($messagedt,$status);
			$ret=str_replace('null','""',$ret);
			$this->apiHelper->getJsonResponse($ret);
		}
		
		//disabling view		
		$this->view->disable();	
	}
	
	private function sptaUpdate($vehicle_no,$driver_name,$vehicle_type,$spta_num)
	{
		$mobspta=new Spta();
		$mobspta->updateSPTA($vehicle_no,$driver_name,$vehicle_type,$spta_num,$gisdata,'_LINE_759_');
		
		return TRUE;
	}
	
	public function checkUpdate($arr)
	{
		$sync=new Syncronize();
		$isUpdated=FALSE;
		
		//Retrieve SPTA row from db;
		$recs=$sync->showsptadata($arr['spta_num'],'harvesting_ticket',date('Ymd'));
		/*echo '<pre>';
		print_r($arr);
		print_r($recs);
		die;*/
		//print_r($recs);
		if(count($recs) > 0){
			if(($arr['vehicle_no'] == $recs[0]['police_number']) 
				&& ($arr['driver_name'] == $recs[0]['driver'])
				&& ($arr['brix'] == $recs[0]['brix'])
				&& ($arr['vehicle_type'] == $recs[0]['vehicle_type'])	
			){
				$isUpdated=TRUE;
				#die($isUpdated);
			}else{
				$isUpdated=FALSE;
			}
		}else{
			$isUpdated=FALSE;
		}
		
		return $isUpdated;
	}
	
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
	
	private function preparenewsptadata($farmerdt,$vehicle_no,$driver_name,$vehicle_type,$brix,$day,$check_point,$truck_status)
	{
		$ret	= []; 
		if(is_array($farmerdt)){
			if(count($farmerdt) > 0){
				$syncmodel 		= new Syncronize();
				foreach($farmerdt as $k => $v){
					$ret['DOCNUM']			= $syncmodel->getNextSptaNumber($day);
					$ret['SOL_REGISTRASI']	= $v['SOL_REGISTRASI'];
					$ret['BASEENTRY']		= $v['BASEENTRY'];
					$ret['BASELINE']		= $v['BASELINE'];
					$ret['SOL_QUANTITY']	= 7000;
					$ret['SOL_STATUS']		= 'A';
					$ret['SOL_NOPOL']		= $vehicle_no;
					$ret['SOL_SUPIR']		= $driver_name;
					$ret['SOL_KENDARAAN']	= $vehicle_type;
					$ret['SOL_DATE']		= $day;
					$ret['SOL_BRIX']		= (int)$brix;
					$ret['SOL_EXPIRY']		= date('Ymd', strtotime("+1 day", strtotime($day)));
					$ret['SOL_CREATEDATE']	= date('Ymd');
					$ret['SOL_SOURCE']		= $check_point['checkpoint']; 
					$ret['SOL_APPROVE_TIMESTAMP']	= date('YmdHis');
					$ret['SOL_LONGITUDE']   = $check_point['longitude'];
					$ret['SOL_LATITUDE']    = $check_point['latitude'];
					$ret['SOL_LOCATION']    = $check_point['location'];
					$ret['TRUCK_STATUS']	= $truck_status;
				}
			}
		}
		return $ret;
	}
	
	/*
	 * Checking mysql data (sugarcanegis), blitar 17 of May 2021 
	 */
	private function checksugarcanegisdata($spta_num=''){
		$sync = new Syncronize();
		
		$sqlcheck 		= new Mysqldatacheck();
		$findme			= $this->request->getPost('findme');
		$criteria		= $this->request->getPost('criteria');
		$ret 			= array(); 
		$recs2 			= array(); 
		
		if($criteria=='harvesting_ticket'){
			$spta_num = $findme;
		}
		if($spta_num){
			$recs2 = $sync->showsptadata($spta_num,'harvesting_ticket',date('Ymd'));
			
		}
		if(count($recs2) > 0){
			if(!empty($recs2[0]['RMI_AREAID'])){
				$field_id	= $recs2[0]['RMI_TYPE'].$recs2[0]['RMI_AREAID'];  	
			}
			
		}
		//modified on 07 agustus 2021
		if($criteria=='field_id'){
			$field_id 	= $findme;
		}	
		
		if(isset($field_id)){
			$field_id = strtoupper($field_id);
			if(substr($field_id,0,1)=='D'){
				$SQL=$this->getDroneID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($field_id,0,1)=='H'){		
				$SQL=$this->getTHijauID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($field_id,0,1)=='M'){	
				$SQL=$this->getTManisID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($field_id,0,1)=='B'){	
				$SQL=$this->getTBibitID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($field_id,0,1)=='V'){	
				$SQL=$this->getTVarietasID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}else if(substr($field_id,0,1)=='P'){	
				$SQL=$this->getTPupukID($field_id);
				$recs = $sqlcheck->rundataquery($SQL);
			}
			
			$brix_scg     = $this->request->getPost('brix_scg');
			if(count($recs) > 0){
				$ret = ['KTP_ID'	 => $recs[0]['ktp_id']
					,'NAME'			 => $recs[0]['name']
					,'RMI_ISVALID' 	 => 'N'
					,'RMI_TYPE'    	 => strtoupper(trim(substr($recs[0]['project'],0,1)))
					#,'RMI_AREAID'  	 => $recs[0]['field_id']
					,'RMI_AREAID'  	 => $recs[0]['petak_id']
					,'RMI_PROJECTID' => $recs[0]['project_id']
					,'RMI_BRIXSCG' 	 => isset($recs2[0]['RMI_BRIXSCG'])?$recs2[0]['RMI_BRIXSCG']:0
					,'PROJECT'		 => $recs[0]['project']
					//,'RMI_ESTTEBANG' => date('Y-m-d',strtotime($recs[0]['estimated_harvesting_day']))
					,'RMI_ESTTEBANG' => isset($recs[0]['estimated_harvesting_day'])
												?date('Y-m-d',strtotime($recs[0]['estimated_harvesting_day']))
												:'0000-00-00'
				];
				if($brix_scg){
					$ret['RMI_BRIXSCG'] = $brix_scg;
				}	
			}
		
		}else{
			$ret = ['KTP_ID'	 	 => 0
					,'NAME'			 => ''
					,'RMI_ISVALID' 	 => 'N'
					,'RMI_TYPE'    	 => 'N'
					,'RMI_AREAID'  	 => 0
					,'RMI_PROJECTID' => 0
					,'RMI_BRIXSCG' 	 => 0
					,'PROJECT'		 => ''
					,'RMI_ESTTEBANG' => '0000-00-00'
				];
			if(isset($brix_scg)){
				$ret['RMI_BRIXSCG'] = $brix_scg;
			}
		}
		return $ret;		
	} 
	
	/*
	 * file upload for scg brix & truck scg 
	 */	
	private function handlescgupload($spta_num){
		#echo '<pre>';
		#echo $spta_num.'##$$$';
		$varallowed 	= ['truck_scg_photo','brix_scg_photo'];
		$vartofilter  	= $_FILES; 
		$yearmod=date('Y');
		$path_upload=$yearmod.'/'.$spta_num[2].$spta_num[3].'/'.$spta_num[4].$spta_num[5];
		$dir='/var/www/test.rejosomanisindo.com/rmi_module/public/spta_upload/'.$path_upload;
		#echo $dir;die('_OE#_');
		if(!file_exists($dir) and !is_dir($dir))
		{				
			mkdir($dir,0775,true);							
		}
		
		foreach($vartofilter as $k => $v){
			if(in_array($k,$varallowed)){
				if($v['error'] <= 0){
					$newname = $k.'_'.$spta_num.'.'.str_replace('.','',substr($v['name'],-4));
					move_uploaded_file($v['tmp_name'], "$dir/$newname") or die('GAGAL UPLOAD');
					unset($_FILES[$k]);
				}
			}
		}
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
			WHERE farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
			#die($SQL);
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
			WHERE farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		#die($SQL);
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
			WHERE farmer_id IS NOT NULL AND T1.ID=\''.$param.'\'';	
		#die($SQL);	
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
	
}
