<?php
namespace Zircon\Modules\Checkpointservices\Controllers;

use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser};
use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
use Zircon\Modules\Checkpointservices\Models\AES;

class LoginController extends ControllerBase
{

    public function indexAction()
    {
		$this->response->redirect('Checkpointservices/login/auth');
    }
	
	public function isMaintenance()
	{
		$check=false;
		
		if($check)
		{
			return "200";
		}else{
			return "404";
		}
	}

	public function authAction()
	{
		//if($this->isMaintenance()===404)
		//{
		if($this->apiHelper->tokenCheck()){
			
			$_usr=($this->request->has('username'))?$this->request->get('username'):null;
			$_pwd=($this->request->has('password'))?$this->request->get('password'):null;
			$_lat=($this->request->has('latitude'))?$this->request->get('latitude'):null;
			$_lon=($this->request->has('longitude'))?$this->request->get('longitude'):null;
			$user=(new OUuser)->getUser($_usr);
		
			try{
				$status=[
					'code'=>'200',
					'sts'=>'Success',
				];
				
				
				if(!empty($_usr) && !empty($_pwd) && $user)
				{
					/*echo '<pre>';
					print_r($user); 
					echo '</pre>';
					die();*/
					$pepper="soltius";
					
					$xpass=(new encdec)->decryptreborn($user['UPASS'],$pepper);
					
					if(empty($user['Emp'])){
						$additional=[];
						$additional['Officer']=3;
					}else{
						if($user['Emp']=='Y'){
							$additional['Officer']=2;
						}elseif($user['Emp']=='N'){
							$additional['Officer']=1;
						}else{
							$additional['Officer']=3;
						}
					}
					
					$radius=0;
					if($_lat !=null && $_lon !=null && $user['Latitude'] && $user['Longitude'])
					{
						$radius=(10>=$this->distHaversine($_lat,$_lon,$user['Latitude'],$user['Longitude']))?
							$this->distHaversine($_lat,$_lon,$user['Latitude'],$user['Longitude'])
							:10;
					}else{
						$radius=$user['Radius'];
					}
					
					if($xpass==$_pwd){
						
						if((bool)$user['active']){
							// TODO ADJUST WITH IMEI
							//if ($user['IS_LOGIN'] == '0' ||
							// ($user['IS_LOGIN'] == '1' && $user['IMEI'] == $_imei)) {
								$pwdaccess=TRUE;
								
								$updateLogin=(new OUuser)->updateIsLogin($_usr, '1');
								$resp['match']=$pwdaccess;
								$resp['callback']=[
									'Username'=>$user['Username'],
									'Name'=>[
										'First'=>(empty($user['FirstName']))?$user['Username']:$user['FirstName'],
										'Middle'=>(empty($user['MiddleName']))?'':$user['MiddleName'],
										'Last'=>(empty($user['LastName']))?'':$user['LastName'],
									],
									'Email'=>(empty($user['Email']))?'':$user['Email'],
									'NrKontak'=>(empty($user['Contact']))?'':$user['Contact'],
									'ViewPosPantau'=>$user['UVPOS'],
									'AddPosPantau'=>$user['UAPOS'],
									'KdPosPantau'=>$user['KdPosPantau'],
									'NmPosPantau'=>($user['NmPosPantau']!=null)?$user['NmPosPantau']:$user['KdPosPantau'],
									'Latitude'=>($user['Latitude']!==null)?$user['Latitude']:'',
									'Longitude'=>($user['Longitude']!==null)?$user['Longitude']:'',
									'PermissibleRadius'=>$radius,
									'TapPerm'=>$user['TapPerm'],
									'PrintSPTAPerm'=>$user['PrintSPTAPerm'],
									// Added 14/07/2020 08:18
									'IsEmplacement'=>$user['Emp'],
									'IsCaneyard'=>$user['Caneyard'],
									// End Added 14/07/2020 08:18
									'EmplOfficer'=>$additional['Officer'],
								];
							// } else if ($user['IMEI'] != $_imei) {
							// 	$pwdaccess=TRUE;
								
							// 	$status['sts']='Mohon maaf, device tidak valid!';
							// 	$resp['match']=$pwdaccess;
							// 	$resp['callback']='Mohon maaf, device tidak valid!';
							// } else {
							// 	$pwdaccess=TRUE;
								
							// 	$status['sts']='Mohon maaf, user anda sedang login!';
							// 	$resp['match']=$pwdaccess;
							// 	$resp['callback']='Mohon maaf, user anda sedang login!';
							// }
						
						
						}else{
							$pwdaccess=TRUE;
							
							$status['sts']='Mohon maaf, untuk sementara user anda sedang tidak aktif';
							$resp['match']=$pwdaccess;
							$resp['callback']='Mohon maaf, untuk sementara user anda sedang tidak aktif';
						}
					}else{
						$pwdaccess=FALSE;
						
						$resp['match']=$pwdaccess;
						//$resp['xpass']=$xpass;
						$resp['callback']='password mismatch';
					}	
					
					
				}else{
					if(!empty($_usr) && !empty($_pwd)){
						$resp['callback']='The username is not found in our system';
					
					}else{
						$resp['callback']='User and Password is not filled';
					}
				}
			}catch(exception $e){
				$resp['callback']=$e->message;
			}
		}else{
			$status=['code' => '401 ','sts' => 'Unauthorized'];
			$resp=['message' => 'No token supplied or token is not valid'];
		}
		$ret=$this->apiHelper->setJsonResponse($resp,$status);
		$this->apiHelper->getJsonResponse($ret);
		/*}elseif($this->isMaintenance()===200){
			$status=['code' => '404','sts' => 'Trouble'];
			$resp=['message' => 'Aplikasi sedang trouble, harap bersabar'];
			
			$ret=$this->apiHelper->setJsonResponse($resp,$status);
			$this->apiHelper->getJsonResponse($ret);
		}*/
		$this->view->disable();	
	}
	
	private function distHaversine($startLat,$startLon,$endLat,$endLon)
	{
		$distance=0;		
		$earth_radius = 6371;
 
		$dLat = deg2rad($endLat - $startLat);
		$dLon = deg2rad($endLon - $startLon);
	 
		$a = sin($dLat/2) * sin($dLat/2) + cos(deg2rad($startLat)) * cos(deg2rad($endLat)) * sin($dLon/2) * sin($dLon/2);
		$c = 2 * asin(sqrt($a));
		$distance = $earth_radius * $c;
		
		return $distance;
	}
	
	private function gDistance($start,$destination)
	{
		//The Google Directions API URL. Do not change this.
		$apiUrl = 'http://maps.googleapis.com/maps/api/directions/json';

		//Construct the URL that we will visit with cURL.
		$url = $apiUrl . '?' . 'origin=' . urlencode($start) . '&destination=' . urlencode($destination);

		//Initiate cURL.
		$curl = curl_init($url);

		//Tell cURL that we want to return the data.
		curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);

		//Execute the request.
		$res = curl_exec($curl);

		//If something went wrong with the request.
		if(curl_errno($curl)){
			throw new Exception(curl_error($curl));
		}

		//Close the cURL handle.
		curl_close($curl);

		//Decode the JSON data we received.
		$json = json_decode(trim($res), true);

		//Automatically select the first route that Google gave us.
		$route = $json['routes'][0];

		//Loop through the "legs" in our route and add up the distances.
		$totalDistance = 0;
		foreach($route['legs'] as $leg){
			$totalDistance = $totalDistance + $leg['distance']['value'];
		}

		//Divide by 1000 to get the distance in KM.
		$totalDistance = round($totalDistance / 1000);

		//Print out the result.
		echo 'Total distance is ' . $totalDistance . 'km';

		//var_dump the original array, for illustrative purposes.
		var_dump($json);
		
		return $totalDistance;
	}
	
	public function logoutAction()
	{
		//if($this->isMaintenance()===404)
		//{
		if($this->apiHelper->tokenCheck()){
			
			$_usr=($this->request->has('username'))?$this->request->get('username'):null;
			$user=(new OUuser)->getUser($_usr);
		
			try{
				$status=[
					'code'=>'200',
					'sts'=>'Success',
				];
				
				
				if(!empty($_usr) && $user)
				{
					/*echo '<pre>';
					print_r($user); 
					echo '</pre>';
					die();*/
					$u=(new OUuser)->updateIsLogin($_usr, '0');
					$pwdaccess=TRUE;
					
					$resp['match']=$pwdaccess;
					//$resp['xpass']=$xpass;
					$resp['callback']='Logout Success';
				}else{
					if(!empty($_usr)){
						$resp['callback']='The username is not found in our system';
					
					}else{
						$resp['callback']='User is not filled';
					}
				}
			}catch(exception $e){
				$resp['callback']=$e->message;
			}
		}else{
			$status=['code' => '401 ','sts' => 'Unauthorized'];
			$resp=['message' => 'No token supplied or token is not valid'];
		}
		$ret=$this->apiHelper->setJsonResponse($resp,$status);
		$this->apiHelper->getJsonResponse($ret);
		$this->view->disable();	
	}
	
}

