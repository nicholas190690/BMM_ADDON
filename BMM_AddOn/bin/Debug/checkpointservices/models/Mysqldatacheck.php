<?php 
/*
 * Bismillahirrahmanirrahim... 				Blitar, 18 of March 2021 
 */
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use Phalcon\Mvc\Model\Query;

class Mysqldatacheck extends Model {
	
	/*public $ID;
	public $LAND_ID; 
	public $COORDINATE;
	public $SURFACE_AREA;
	public $SUGAR_CANE_CATEGORY; 
	public $STATUS; 
	public $CREATED;
	public $CREATED_BY; 
	public $LAST_UPDATE; 
	public $UPDATED_BY; 
	public $TAXATION; 
	public $SUGAR_CANE_VARIETIES;
	public $SURVEY_DATE; 
	public $SOIL_TYPE;
	public $WATER_SOURCES;
	public $PLANTING_HISTORY;
	public $SUGAR_CANE_CLASS;
	public $DISTANCE_TO_FACTORY;
	public $PHOTO;
	public $SAP_LAND_AREA_ID;
	public $TRACKED;
	public $PLANTING_DATE; 
	public $FIELD_ID;
	public $ALT; 
	public $YIELD;
	public $X; 
	public $Y;
	public $HARVESTED;
	public $PROJECT;*/
	/*
	 * setting table name 
	 */
	public function initialize()
    {
		$this->setConnectionService('GIS');
        #$this->setSource('FARMER_LAND_AREA');
    }
	
	public function rundataquery($SQL){
		$connection = $this->getDi()->getShared('GIS');
		$recs 		= $connection->query($SQL);
		$recs = $recs->fetchAll($recs);
		
		return $recs;
	}
}