<?php
namespace Zircon\Modules\Checkpointservices\Controllers;

#use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser,Sptalog};
#use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
#use Zircon\Modules\Checkpointservices\Models\{AES,Spta,Syncronize};

//added untuk penyesuaian 
#use Zircon\Modules\Checkpointservices\Models\Mysqldatacheck; 
use Zircon\Modules\Checkpointservices\Models\Truck; 

class TruckregistrationController extends ControllerBase
{
	
	public function indexAction()
	{
		//die('Trouble');
		#phpinfo();
		$tokensts=$this->apiHelper->tokenCheck();
		
		try{
			if($tokensts){				
				if($this->request->isPost()){ 
					$police_number     = $this->request->getPost('police_number'); 
					$dt 	= $this->savetruckdata($police_number);
					$status 	= $dt['status'];
					$messagedt  = $dt['messagedt']; 	
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

	/*
	 * upload action 
	 */
	private function uploadphotos($police_number){
		$truckdt 	= [];
		if ($this->request->hasFiles() == true) {
			
			//--------------------------
			$dirloc		= '/var/www/zircon/public/truckphotos/'.date('Ym').'/';
			if (!file_exists($dirloc)){
				mkdir( $dirloc,0777,true) or die('Permission denied');
				chmod($dirloc,0777);
			}
			
			$vehicle_type     = $this->request->getPost('vehicle_type'); 
			$latitude		  = $this->request->getPost('latitude'); 
			$longitude		  = $this->request->getPost('longitude'); 
			$location		  = $this->request->getPost('location'); 
			
			$truckdt['police_number'] 	= $police_number;
			$truckdt['vehicle_type'] 	= $vehicle_type; 
			$truckdt['latitude'] 		= $latitude; 
			$truckdt['longitude'] 		= $longitude; 
			$truckdt['location'] 		= $location; 
			//--------------------------
			foreach ($this->request->getUploadedFiles() as $file) {
				$keyname 	=  $file->getKey();
				$checkname  = substr($keyname,0,strpos($keyname, '.'));
				if(!$checkname){
					$checkname = $keyname;
				}
				$newname	= $police_number.'_'.$keyname.'_'.date('Ymdhis').'.'.$file->getExtension();
				$errcheck 	= $file->getError();
				if($errcheck <= 0){
					$file->moveTo($dirloc.$newname);
					if(file_exists($dirloc.$newname)){
						//------
						$truckdt[$keyname] = $dirloc.$newname; 
						//------
					}
				}				
			}			
		}	
		
		return $truckdt;
	}	
	
	private function savetruckdata($police_number){
		$dt 				= $this->uploadphotos($police_number); 
		$username     		= $this->request->getPost('usernames'); 
		$checkus 			= Truck::findFirst([
								'conditions' => 'police_number=\''.$police_number.'\'',
								'for_update' => false, // With this option, Phalcon\Mvc\Model reads the latest available data, setting exclusive locks on each row it reads
							]);
		$status		= ['code'=>'401',
						'sts'=>'Unauthorized'];
		$messagedt	=['message'=>'Failed'];			
		
		if(!isset($checkus->id)){
			if(isset($dt['front_photo'])){
				$truck = new Truck();
				$truck->police_number 	= strtoupper(preg_replace('/\s+/', '',  $dt['police_number']));
				$truck->vehicle_type	= $dt['vehicle_type'];
				$truck->front_photo 	= isset($dt['front_photo'])?$dt['front_photo']:'';
				$truck->left_photo 		= isset($dt['left_photo'])?$dt['left_photo']:'';
				$truck->right_photo 	= isset($dt['right_photo'])?$dt['right_photo']:'';
				$truck->back_photo 		= isset($dt['back_photo'])?$dt['back_photo']:'';
				$truck->stnk_photo 		= isset($dt['stnk_photo'])?$dt['stnk_photo']:'';
				$truck->latitude 		= isset($dt['latitude'])?$dt['latitude']:'';
				$truck->longitude 		= isset($dt['longitude'])?$dt['longitude']:'';
				$truck->location 		= isset($dt['location'])?$dt['location']:'';
				$truck->created 		= date('Y-m-d H:i:s');
				$truck->created_by 		= $username;
				
				if ($truck->save() === false) {
					#echo "Umh, We can't store robots right now: \n";
					/*$messages = $truck->getMessages();
					foreach ($messages as $message) {
						echo $message, "\n";
					}*/
					$status=[
						'code'=>'404',
						'sts'=>'failed'
					];
					$messagedt=['message'=> 'Gagal Simpan'];
				} else {
					$status=[
						'code'=>'200',
						'sts'=>'success'
					];
					$messagedt=['message'=> $police_number];
				}
			}else{
				$status=[
					'code'=>'401',
					'sts'=>'Unauthorized'
				];
				$messagedt=['message'=>'Upload Failed'];
			}
		}else{
			$status=[
					'code'=>'401',
					'sts'=>'Unauthorized'
				];
			$messagedt=['message'=>'Truck data exist'];
		}
		
		return ['status' => $status,'messagedt' => $messagedt];
	}
	
	public function gettruckdataAction(){
		$tokensts=$this->apiHelper->tokenCheck();
		if($tokensts){				
			if($this->request->isPost()){ 
				$police_number 	= $this->request->getPost('police_number');
				$police_number	=  strtoupper(preg_replace('/\s+/', '',  $police_number));
				
				$checkus 		= Truck::findFirst([
									'conditions' => 'police_number=\''.$police_number.'\'',
									'for_update' => false, // With this option, Phalcon\Mvc\Model reads the latest available data, setting exclusive locks on each row it reads
								]);
				if(isset($checkus->id)){
					$dt = [['id'			=> $checkus->id,
							'police_number'	=> str_replace('/var/www/zircon/public/',
							'http://api.rejosomanisindo.com:6026/',$checkus->police_number),
							'vehicle_type'	=> $checkus->vehicle_type,
							'front_photo'	=> str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$checkus->front_photo),
							'left_photo'	=> str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$checkus->left_photo),
							'right_photo'	=> str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$checkus->right_photo),
							'back_photo'	=> str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$checkus->back_photo),
							'stnk_photo'	=> str_replace('/var/www/zircon/public/','http://api.rejosomanisindo.com:6026/',$checkus->stnk_photo),
							]
						]; 
					/*echo '<pre>';
					print_r($checkus); 
					echo '</pre>';
					die;*/
					
					$status=[
						'code'=>'200',
						'sts'=>'success'
					];
					$messagedt = $dt;
					
				}else{
					$status=[
						'code'=>'401',
						'sts'=>'no-data'
					];
					$messagedt=['message'=>'No data found'];
				}
			}else{
				$status=['code'=> '405',
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
	}
	
}
