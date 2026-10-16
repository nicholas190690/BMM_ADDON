<?php
/*
 * Blitar, 30 04 2020 
 * Controller Description : handle Queue ticket print, it will also insert to RMI_ADDON_MASTER."SOL_POS" 
 */
 
namespace Zircon\Modules\Checkpointservices\Controllers;
use ReallySimpleJWT\Token;
use Zircon\Modules\Checkpointservices\Models\Syncronize; 

class PrintqueueController extends ControllerBase {
	
	public function indexAction(){
		$this->view->setLayout('mobileprint');
		
		/*$findme			= $this->request->getQuery('findme');
		$criteria		= $this->request->getQuery('criteria');
		$check_point	= $this->request->getQuery('check_point');
		$type			= $this->request->getQuery('type');*/
		
		/*echo '<pre>';
		print_r($_GET); 
		echo '</pre>';
		die('_SKIP_');*/ 
		
		$findme			= $this->request->getPost('findme');
		$criteria		= $this->request->getPost('criteria');
		$check_point	= $this->request->getPost('check_point');
		$type			= $this->request->getPost('type');
		$day			= date('Ymd');
		
		$tokensts	=	$this->tokenCheck();
		//remove nanti 
		$tokensts	=	true;
		
		if($tokensts){
			$syncmodel 		= new Syncronize();  
			$parkingcheck = $syncmodel->validateparkingarea($check_point);
			#die($parkingcheck); 
			if($parkingcheck=='Y'){				
				$recs			= $syncmodel->showsptadata($findme,$criteria,$day);
				/*echo '<pre>';
				print_r($recs); 
				echo '</pre>';
				die;*/
				//saving to SOL_POS 
				if(count($recs) > 0){
					$cptransaction = $syncmodel->savecheckpoint($recs[0]['harvesting_ticket'],$type,$check_point);
					if($cptransaction=='ok'){
						//print queue ticket on parking are in gate 
						if($type=='IN'){
							//altered on 21 of May 2020 
							/*$queuenumber 	= $syncmodel->getinsertedqueuenumber($recs[0]['harvesting_ticket']);
							$this->view->queuenumber	= $queuenumber;*/
							$dt			= $syncmodel->getinsertedqueuenumber($recs[0]['harvesting_ticket']);
							$quotacheck = $syncmodel->checkfivetythousandquota(); 
							
							/*echo '<pre>';
							print_r($dt); 
							#print_r($quotacheck); 
							echo '</pre>'; 
							die('_SKIP_');*/
							
							$this->view->queuenumber	= '';
							$this->view->parkingarea	= '';
							if(count($dt) > 0){
								$this->view->queuenumber	= $dt['SOL_ANTRIAN'];
								$this->view->parkingarea	= $dt['NAME'];
								$this->view->urutantruck	= $dt['TRUCK_KE'];
								$this->view->quotacheck		= $quotacheck;
								$this->view->spta			= $dt['SOL_SPTA'];
								$this->view->nopol			= $dt['SOL_NOPOL'];
							}else{
								$this->view->queuenumber	= '-';
								$this->view->parkingarea	= '-';
								$this->view->urutantruck	= '-';
								$this->view->quotacheck		= '-';
								$this->view->spta			= '-';
							}
							echo $this->view->partial('printqueue/queueticket');
						}else{
							//out parking area return 
							$status=['code' => '200 ','sts' => 'success'];
							$messagedt=['message' => 'recorded'];
							$ret 		= $this->setJsonResponse($messagedt,$status);		
							$this->getJsonResponse($ret);
							//disabling view 
							$this->view->disable();
						}							
					}else{
						echo $this->view->partial('printqueue/error');	
					}
				}else{
					echo $this->view->partial('printqueue/error');	
				}
			}	
		}else{
			echo $this->view->partial('printqueue/error');	
		}
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
}