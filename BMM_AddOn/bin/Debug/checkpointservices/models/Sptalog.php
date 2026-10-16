<?php
/*
 * 11 September 2020 02:03hr
 */
 namespace Zircon\Modules\Checkpointservices\Models;
 use Phalcon\Mvc\Model;
 use Phalcon\Mvc\Model\Query;
 
 class Sptalog extends Model {
	
	public function initialize()
    {
        $this->setConnectionService('dbCheckpoint');
    }
	
	/*
	 * Get Sptalog data  
	 */	
	public function gettabledata($sql){
		$connection = $this->getDi()->getShared('dbCheckpoint');
		$recs 		= $connection->query($sql);
		
		return $recs;
	}
	
    /*
	 * inserting into 
	 */
	 
	public function logger($logparams=null)
	{
		//
		$dir='log';
		
		if(!file_exists($dir) and !is_dir($dir)){
			mkdir($dir,0775,true);
		}
		
		$date=date('Ymd',time());
		$time=date('H:i:s',time());
		$logname='log_'.$date;
					
		if(is_array($logparams)){
			$cp='';
			
			if(array_key_exists('checkpoint',$logparams)) {
				$cp='__'.$logparams['checkpoint'];
			}
			
			$logname.=$cp;
			if(!file_exists($logname)){
				$logname=$logname.'.log';
				file_put_contents($logname,'');
			}
		}else{
			if(!file_exists($logname)){
				$logname=$logname.'.log';
				file_put_contents($logname,'');
			}
		}
		
		$contents=file_get_contents($logname);
		$contents.="$time\t$log\r";
		
		file_put_contents($logname,$contents);
	}
	
 }	