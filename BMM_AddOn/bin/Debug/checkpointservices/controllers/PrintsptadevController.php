<?php
/*
 * Blitar, 30 04 2020 
 * Controller Description : handle SPTA (harvesting ticket) print, it will also insert to RMI_ADDON_MASTER."SOL_POS" 
 */
 
namespace Zircon\Modules\Checkpointservices\Controllers;
use ReallySimpleJWT\Token;
use Zircon\Modules\Checkpointservices\Models\Syncronizedev;
use Zircon\Modules\Checkpointservices\Models\Projectsetting; 

class PrintsptadevController extends ControllerBase {
	
	public function indexAction(){
		$this->view->setLayout('mobileprint');
		
		//------added on 13 of April 2022-----
		$projectsetting = new Projectsetting(); 
		$projectdata 	= $projectsetting->getprojectsetting();	
		//------------------------------------
		
		/*$findme			= $this->request->getQuery('findme');
		$criteria		= $this->request->getQuery('criteria');
		$check_point	= $this->request->getQuery('check_point');
		$type			= $this->request->getQuery('type');*/
		
		$findme			= $this->request->getPost('findme');
		$criteria		= $this->request->getPost('criteria');
		$check_point	= $this->request->getPost('check_point');
		
		if(isset($_GET['criteria'])){
			$findme			= $this->request->getQuery('findme');
			$criteria		= $this->request->getQuery('criteria');
			$check_point	= $this->request->getQuery('check_point');
			$type			= $this->request->getQuery('type');
			#echo $findme.$criteria.'--'.$check_point.'--'.$type;die;
		}
		
		$type			= $this->request->getPost('type');
		
		$day			= date('Ymd');
		$tokensts		= $this->tokenCheck();
		
		//remove nanti 
		$tokensts	=	true;
		
		if($tokensts){
			$syncmodel 		= new Syncronizedev();  
			$recs			= $syncmodel->showsptadata($findme,$criteria,$day);
			echo '<pre>';
			print_r($syncmodel->checkritaseuse('M11874'));
			echo '</pre>';
			die;
			/*echo '<pre>';
			print_r($recs); 
			echo '</pre>';
			die('_SKIP_');*/
			//saving to SOL_POS 
			if(count($recs) > 0){
					$checkparking = $syncmodel->validateparkingarea($check_point);
					#die($checkparking); 
					#die;
					$this->view->checkparking = $checkparking;	
				if($checkparking=='N'){
					$cptransaction = $syncmodel->savecheckpoint($recs[0]['harvesting_ticket'],$type,$check_point);
					if($cptransaction=='ok'){
						$this->view->recs	= (count($recs) > 0)?$recs[0]:[];
						$this->view->projectdata = $projectdata;
						echo $this->view->partial('printspta/fixdataspta');		
					}else{
						echo $this->view->partial('printspta/error');	
					}
				}else{
					$this->view->recs	= (count($recs) > 0)?$recs[0]:[];
					echo $this->view->partial('printspta/fixdataspta');		
				}
			}else{
				echo $this->view->partial('printspta/error');	
			}			
		}else{
			echo $this->view->partial('printspta/error');	
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