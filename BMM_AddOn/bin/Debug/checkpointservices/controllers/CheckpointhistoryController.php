<?php
/*
 * 08 May 2020 08:39
 *
 */
namespace Zircon\Modules\Checkpointservices\Controllers;

use Zircon\Modules\Checkpointservices\Models\Checkpoint;
//--tambahan agar bisa tangkap tokenservices
/*use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser};
use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
use Zircon\Modules\Checkpointservices\Models\{AES,Spta,Syncronize};*/


class CheckpointhistoryController extends ControllerBase
{
    public function indexAction()
    {
		$tokensts = $this->apiHelper->tokenCheck();
		
		try{
			if($tokensts)
			{			
				if($this->request->isPost()){
					$cpid		= $this->request->get('checkpoint');
					$datefrom	= ($this->request->has('datefrom'))?$this->request->get('datefrom'):date('Ymd');
					$dateto		= $this->request->get('dateto');
					if(strtotime($dateto)>strtotime($datefrom)){
						$dateto	= ($this->request->has('dateto'))?$this->request->get('dateto'):date('Ymd');
					}else{
						$dateto	= date('Ymd');
					}
					
					$cp = new Checkpoint();
					if($cpid)
					{
						$cp=$cp->cphistory($cpid,$datefrom,$dateto);
						
						$status=[
							'code'=>'200',
							'sts'=>'Success'
						];
						$messagedt = ['data' => $this->data($cp)];
					}else{
						$status = [
							'code'=>'400',
							'sts'=>'Bad Request'
						];
						$messagedt = ['message'=>'Wrong Parameter supplied. \'Checkpoint\' is required, whilst \'datefrom\' and \'dateto\' is supplementary.'];
					}
					
				}else{
					$status=[
						'code' => '405',
						'sts'  =>'Method Not Allowed'
					];
					$messagedt = ['message'=>'The method received in the request-line is known by the origin server but not supported by the target resource.'];	
				}
			}
			else{
				$status=[
					'code'	=>	'401',
					'sts'	=>	'Unauthorized'
				];
				$messagedt	=	['message'=>'No token supplied or token is not valid'];
			}
			
			$ret	=	$this->apiHelper->setJsonResponse($messagedt,$status);
			$this->apiHelper->getJsonResponse($ret);
		}catch(exception $e)
		{
			$status=[
				'code'	=>	$e->code,
				'sts'	=>	$e->message,
			];
			$messagedt	=	['message'=>'Error'];
			$ret	=	$this->apiHelper->setJsonResponse($messagedt,$status);
			$ret	=	str_replace('null','""',$ret);
			$this->apiHelper->getJsonResponse($ret);
		}
		//disabling view 
		$this->view->disable();
    }
	
	private function data($arr)
	{
		
		$out=[];
		foreach($arr as $key => $in)
		{
			$out[$key]['index']=$key;
			$out[$key]['tgl_waktu']=$this->datetrim($in['SOL_DATE']).' '.$this->timemod($in['SOL_TIME']);
			$out[$key]['pospantau']=$in['NAME'];
			$out[$key]['spta']=$in['SOL_SPTA'];
			$out[$key]['nm_petani']=$in['SOL_NAMA'];
			$out[$key]['nm_sopir']=$in['SOL_SUPIR'];
			$out[$key]['nopol_kendaraan']=$in['SOL_NOPOL'];
            //"SOL_ISEMPLACEMENT": "N",
            //"SOL_SUPIR": "IKSAN",
            //"SOL_NOPOL": "AG8483A"
		}
		
		$out_ct=count($out);
		$_out=$out;
		$out=[];
		$j=0;
		for($i=$out_ct-1;$i>-1;$i--)
		{
			$out[$j]=$_out[$i];
			$out[$j]['index']=$j;
			$j++;
		}
		
		//$out=$arr[49];
		return $out;
	}
	
	private function datetrim($date_in)
	{
		$out="";
		for($i=0;$i<10;$i++){
			$out.=$date_in[$i];
		}
		
		return $out;
	}
	
	private function timemod($time_in)
	{
		$out="";
		$len=strlen($time_in);
		
		switch($len){
			case 1:$out="00:0".$time_in[0];break;
			case 2:$out="00:".$time_in[0].$time_in[1];break;
			case 3:$out="0".$time_in[0].":".$time_in[1].$time_in[2];break;
			case 4:$out=$time_in[0].$time_in[1].":".$time_in[2].$time_in[3];break;
		}
		
		return $out;
	}
	
}

