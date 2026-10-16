<?php
/*
 * Blitar, 14 05 2020					Bismillahirrahmanirrahim.. 
 */
 
namespace Zircon\Modules\Checkpointservices\Controllers;
use ReallySimpleJWT\Token;
use Zircon\Modules\Checkpointservices\Models\Gpslog; 

class GpslogController extends ControllerBase
{
		public function indexAction(){
			if($this->apiHelper->tokenCheck()){
				
				$checkpoint 	= $this->request->getPost('checkpoint');
				$latitude		= $this->request->getPost('latitude');
				$longitude		= $this->request->getPost('longitude');
				$location		= htmlspecialchars($this->request->getPost('location'));
				$logingtype		= $this->request->getPost('logingtype');
				$createddate	= date('Y-m-d H:i:s');
				
				$check 			= $this->savegpslog($checkpoint,$latitude,$longitude,$location,$logingtype,$createddate);
				#----
				if($check){
					$status	=	['code' => '200 ','sts' => 'success'];
					$resp	=	['message' => 'log saved'];
				}else{
					$status	=	['code' => '500 ','sts' => 'Failed'];
					$resp	=	['message' => 'gagal log'];	
				}
				
			}else{
				$status	=	['code' => '401 ','sts' => 'Unauthorized'];
				$resp	=	['message' => 'No token supplied or token is not valid'];
			}
			
			$ret=$this->apiHelper->setJsonResponse($resp,$status);
			$this->apiHelper->getJsonResponse($ret);
		
			$this->view->disable();	
		}
		
		private function savegpslog($checkpoint,$latitude,$longitude,$location,$logingtype,$createddate){
			if($checkpoint && $latitude && $longitude && $location && $logingtype && $createddate){
				$gpslog	= new Gpslog();
				
				$gpslog->checkpoint	= $checkpoint;
				$gpslog->latitude	= $latitude;
				$gpslog->longitude	= $longitude;
				$gpslog->location	= $location;
				$gpslog->logingtype	= $logingtype;
				
				$check = $gpslog->create();
				
				return $check; 
			}
		}
}
