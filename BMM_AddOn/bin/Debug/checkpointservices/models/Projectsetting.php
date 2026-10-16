<?php 
/*
 * Bismillahirrahmanirrahim... 				Blitar, 18 of March 2021 
 */
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use Phalcon\Mvc\Model\Query;

class Projectsetting extends Model {
	
	public $id;
	public $project_name; 
	public $project_prefix;
	public $marking_brix;
	public $check_point_brix; 
	public $emplasemen_point_brix; 
	public $start_period;
	public $end_period; 
	public $created; 
	public $created_by; 
	public $updated; 
	public $updated_by;
	/*
	 * setting table name 
	 */
	public function initialize()
    {
		$this->setConnectionService('GIS');
        $this->setSource('PROJECT_SETTING');
    }
	
	/*
	 * Get project info 
	 */
	public function getprojectsetting(){
		$ret  = [];  
		$SQL  = 'SELECT * FROM PROJECT_SETTING 
		WHERE DATE(start_period) <= CURRENT_DATE() AND DATE(end_period) >= CURRENT_DATE()
		GROUP BY project_prefix';
		
		$SQL2 = 'SELECT * FROM CRUSHING_PERIOD WHERE CRUSHING_PERIOD='.date('Y');
		
		$connection = $this->getDi()->getShared('GIS');
		$recs 		= $connection->query($SQL);
		$recs 		= $recs->fetchAll($recs);
		
		$recs2 		= $connection->query($SQL2);
		$recs2 		= $recs2->fetchAll($recs2);
		
		if(count($recs) > 0){
			foreach($recs as $k => $v){
				$ret[$v['project_prefix']] = ['marking_brix' 			=> $v['marking_brix'],
											  'project_name' 			=> $v['project_name'],	
											  'check_point_brix' 		=> $v['check_point_brix'],
											  'emplasemen_point_brix' 	=> $v['emplasemen_point_brix'],
											  'minimum_cutting_days' 	=> $v['minimum_cutting_days'],
											  'e_marking_date' => $v['e_marking_date'],
											  'e_max_cut_aftr_crushing' => $v['e_max_cut_aftr_crushing'],
											  'project_prefix' => $v['project_prefix'],
											  'crushing_start' => isset($recs2[0]['crushing_start'])?substr($recs2[0]['crushing_start'],0,10):''
											];
			}
		}
		
		return $ret;
	}
	
	public function rundataquery($SQL){
		$connection = $this->getDi()->getShared('GIS');
		$recs 		= $connection->query($SQL);
		$recs = $recs->fetchAll($recs);
		
		return $recs;
	}
}