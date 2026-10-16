<?php
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use stdClass;
use Zircon\hanaAdapter as hanaAdapter;

class CheckVersion extends Model
{
	private $fetch;
	private $conn;
	public $a_table;
	
	public function initialize($fetch=[])
	{
		$this->fetch=$fetch;
		
		$this->a_table="RMI_ADDON_MASTER";
		#$this->a_table='ADDONUAT2021';
		//brongkos:439876
	}
	
	/*
	 * Getting hana connection using odbc
	 */
	private function getHanaConn(){
		$hdbodbc = new hanaAdapter();
		$this->conn = $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
		
		return $this->conn;
	}
	
	public function getVersion($version)
	{
		if(!empty($version)||$version!=null)
		{
            $codeapp = "MACP";
            $sql='SELECT * FROM '.$this->a_table.'."RMI_APPVERSION" WHERE "CODEAPP" = \''.$codeapp.'\' AND "VERSION" >= \''.$version.'\'
            AND ACTIVE = 1 ORDER BY VERSION DESC ';
				
			$records=(new hanaAdapter)->runcustomquery($sql,$this->getHanaConn());
			
			if(count($records)>0){
				$this->fetch=$records[0];
			}else{
				$this->fetch=$records;
			}
		}else{
			if(empty($version))
			{
				$this->fetch="version is empty";
			}else if($user==null){
				$this->fetch="its null";
			}
		}
		
		return $this->fetch;
	}
	
}
