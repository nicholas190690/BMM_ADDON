<?php
namespace Zircon\Modules\Tokenservices\Controllers;

use ReallySimpleJWT\Token;

class GetRestTokenController extends ControllerBase
{
	
	private $tsecret;
	
	//Generate new token 
	public function indexAction()
    {
		#die('_SKIP_');
		$this->setTokenSecret();
		
		$payload = [
			'iat' => time(),
			'uid' => 1,
			'exp' => time() + 300,
			'iss' => 'localhost'
		];

		$token 		= Token::customPayload($payload, $this->tsecret);
		#$token		= 'test';
		die($token);
		$tokendt	= ['token' 		=> $token,
					   'expired on'	=> date('Y-m-d H:i:s',$payload['exp'])	
					];
		
		$status		= !empty($token)
					?['code' => 200,'sts' => 'success']
					:['code' => 204,'sts' => 'no-content'];
		
		$ret 		= $this->setJsonResponse($tokendt,$status);
		
		//ouputing to json 
		$this->getJsonResponse($ret);
		die();
		
	}
	
	public function validateTokenAction(){
		
		$this->setTokenSecret();
		
		$token		= $this->request->getPost('token');
		
		$stscheck 	= ($token!=null)?Token::validate($token, $this->tsecret):die("no token supplied");
		
		$status		= ($stscheck)
					?['code' => '200','sts' => 'Valid']
					:['code' => '401 ','sts' => 'Unauthorized'];
		$messagedt	= ($stscheck)
					?['message' => 'Token is valid']
					:['message' => 'Regenerate token'];
					
		$ret 		= $this->setJsonResponse($messagedt,$status);
		
		$this->getJsonResponse($ret);
		die();
	}
	
	private function setTokenSecret(){
		$this->tsecret	= 'T3l0T3n4nR3k$'; 
	}

	/*
	 * formating json request 
	 */
	private function setJsonResponse($dt,$status){
		
		$tmpret = ['status' 	=> $status,
				   'data' 		=> $dt 
				];
		$ret 	= json_encode($tmpret);
		
		return $ret;
	}
	
	/*
	 * Outputing to json header 
	 */
	private function getJsonResponse($ret){
		header("Content-disposition: attachment; filename=gettokenresp.json;charset=utf-8");
		header('Content-type: application/json');
		echo $ret;
	}
}

