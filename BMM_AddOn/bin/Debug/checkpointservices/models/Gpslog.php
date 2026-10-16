<?php
/*
 * 14/05/2020					Bismillahirrahmanirrahim..
 */
 namespace Zircon\Modules\Checkpointservices\Models;
 use Phalcon\Mvc\Model;
 use Phalcon\Mvc\Model\Query;
 
 class Gpslog extends Model {
	public function initialize()
    {
        $this->setConnectionService('dbCheckpoint');
    }
	
	/*
	 * Get Gpslog data  
	 */	
	public function gettabledata($sql){
		$connection = $this->getDi()->getShared('dbCheckpoint');
		$recs 		= $connection->query($sql);
		
		return $recs;
	}
	
    /*
	 * inserting into 
	 */	
 }	