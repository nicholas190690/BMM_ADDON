<?php
namespace Zircon\Modules\Checkpointservices\Controllers;

use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser,CheckVersion};
use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
use Zircon\Modules\Checkpointservices\Models\AES;
//use Zircon\Models\Checkver;

class VersionController extends ControllerBase
{

    public function indexAction()
    {
		$this->response->redirect('Checkpointservices/version/auth');
    }

	public function authAction()
	{
		if($this->apiHelper->tokenCheck()){
			
			$_ver=($this->request->has('version'))?$this->request->get('version'):null;
			$version=(new CheckVersion)->getVersion($_ver);
		
			Try{
				$status=[
					'code'=>'200',
					'sts'=>'Success',
				];
				
				if(!empty($_ver) && $version)
				{	
						if($version['ACTIVE'] == '1') {
							$veraccess=TRUE;
							
							$resp['match']=$veraccess;
							$resp['callback']=[
								'Id'=>$version['ID'],
								'CodeApp'=>(empty($version['CODEAPP']))?'':$version['CODEAPP'],
								'CodeName'=>(empty($version['CODENAME']))?'':$version['CODENAME'],
								'Version'=>$version['VERSION'],
								'UriFile'=>$version['URIFILE'],
								'Active'=>$version['ACTIVE'],
								'CreateDate'=>(empty($version['CREATEDATE']))?'':$version['CREATEDATE'],
							];
							//$resp['removable_comment']='hayoo kuncine bener...'.$xpass.' piye hayoo~';
							//$resp['w']=$ypass;
							
						} else{
							$veraccess=TRUE;
							
							$status['sts']='Mohon maaf, versi aplikasi anda sudah tidak aktif!';
							$resp['match']=$veraccess;
							$resp['callback']='Silahkan download versi terbaru!';
						}			
					
				}else{
					if(!empty($_usr) && !empty($_pwd) && !empty($version)){
						$resp['callback']='Version is not found in our system';
					
					}else{
						$resp['callback']='Version is not filled';
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
	
}

