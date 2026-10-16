<?php
/*
 * 14/05/2020					Bismillahirrahmanirrahim..
 */
 namespace Zircon\Modules\Checkpointservices\Models;
 use Phalcon\Mvc\Model;
 use Phalcon\Mvc\Model\Query;
 
 class Queue extends Model {
	public function initialize()
    {
        $this->setConnectionService('QUEUE');
    }
	
	/*
	 * Get Gpslog data  
	 */	
	public function gettabledata($sql){
		$connection = $this->getDi()->getShared('QUEUE');
		$recs 		= $connection->query($sql);
		$recs 		= $recs->fetchAll($recs);
		return $recs;
	}
	
    
 }	