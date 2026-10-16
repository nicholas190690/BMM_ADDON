<?php
namespace Zircon\Modules\Checkpointservices\Controllers;

use Zircon\Modules\Checkpointservices\Models\{SymmetricEncryption as encdec,OUuser,Production};
use Zircon\Modules\Checkpointservices\Models\{InvalidHashException,CannotPerformOperationException,PasswordStorage};
use Zircon\Modules\Checkpointservices\Models\AES;
//use Zircon\Models\Checkver;

class ProductionController extends ControllerBase
{

    public function indexAction()
    {
		$this->response->redirect('Checkpointservices/production/data');
    }

	public function dataAction()
	{
		if($this->apiHelper->tokenCheck()){
			
			$production=(new Production)->getProduction();
		
			Try{
				$status=[
					'code'=>'200',
					'sts'=>'Success',
				];
				
				if($production)
				{	
					$resp['callback']=$production;
				}else{
					$resp['callback']='Data is not found';
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

