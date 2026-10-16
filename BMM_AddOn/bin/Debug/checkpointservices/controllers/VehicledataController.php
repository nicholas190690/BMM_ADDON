<?php
/*
 * Blitar, 18 04 2020 
 * Controller Description : get vehicle data 
 */
namespace Zircon\Modules\Checkpointservices\Controllers;
use ReallySimpleJWT\Token;
use Zircon\Modules\Checkpointservices\Models\General; 

class VehicledataController extends ControllerBase
{
	private $a_table;
	private $c_table; 
	
	public function indexAction(){
		
		$tokensts=$this->tokenCheck();
		if($tokensts){
			$status		= ['code' => '200 ','sts' => 'Success'];
			$messagedt	= $this->getvehicledata();
		}else{
			$status=['code' => '401 ','sts' => 'Unauthorized'];
			$messagedt=['message' => 'No token supplied or token is not valid'];
		}
		
		$ret 		= $this->setJsonResponse($messagedt,$status);
		$this->getJsonResponse($ret);
		//disabling view 
		$this->view->disable();
		
	}
	
	private function getvehicledata(){
		$ret	= []; 
		$gm		= new General(); 
		$this->c_table	= 'RMI';
		$SQL 	= 'SELECT * FROM '.$this->c_table.'."@SOL_JENISKENDARAAN" T0 WHERE T0."U_Status"=\'1\'';
		$recs 	= $gm->runCustomQuery($SQL);
		
		if(count($recs) > 0){
			foreach($recs as $k => $v){
				$ret[] = ['vehicle_id' => $v['Code'],'vehicle_name' => $v['Name']];
			}
		}
		
		return $ret;
	}
	
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