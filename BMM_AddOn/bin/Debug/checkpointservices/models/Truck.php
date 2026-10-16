<?php 
/*
 * Bismillahirrahmanirrahim... 				Blitar, 09 of April 2021 
 */
namespace Zircon\Modules\Checkpointservices\Models;
use Phalcon\Mvc\Model;
use Phalcon\Mvc\Model\Query;

class Truck extends Model {
	
	public $id;
	public $police_number;
	public $vehicle_type;
	public $front_photo;
	public $left_photo;
	public $right_photo;
	public $back_photo;
	public $stnk_photo;
	public $latitude; 
	public $longitude; 
	public $location;
	public $created;
	public $created_by;
	
	/*
	 * setting table name 
	 */
	public function initialize()
    {
		$this->setConnectionService('GIS');
        $this->setSource('TRUCKS');
    }
	
	public function rundataquery($SQL){
		$connection = $this->getDi()->getShared('GIS');
		$recs 		= $connection->query($SQL);
		$recs = $recs->fetchAll($recs);
		
		return $recs;
	}
}