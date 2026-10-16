<?php 
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use stdClass;
use Zircon\hanaAdapter as hanaAdapter;

class Checkpoint extends Model
{
	private $a_table;
	private $c_table; 
	
	public function initialize(){
		#TEST_TABLE 
		#$this->a_table	= 'UAT_ADDON2';
		
		#LIVE TABLE 
		$this->a_table	= 'RMI_ADDON_MASTER';
		#$this->a_table='ADDONUAT2021';
		#$this->c_table	= 'UAT2';
		
		#LIVE TABLE 
		$this->c_table	= 'RMI';
		#$this->c_table='UAT2021';
	}
	
	/*
	 * Getting hana connection using odbc
	 */
	private function getHanaConn(){
		$hdbodbc = new hanaAdapter();
		$this->conn = $hdbodbc->hdbconnect("192.168.1.19:30015","","SOLTIUS","P@ssw0rd");
		
		return $this->conn;
	}
	
	function cphistory($cpid,$datefrom,$dateto){
		$sql='SELECT
			T0."SOL_SPTA", 
			T0."SOL_DATE",
			T0."SOL_TIME",
			T1."NAME",
			T1."SOL_ISEMPLACEMENT",
			T3."SOL_NAMA",
			T2."SOL_SUPIR",
			T2."SOL_NOPOL"
			FROM '.$this->a_table.'."SOL_POS" T0
			LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 ON T0."SOL_POSPANTAU" = T1."CODE"
			LEFT JOIN '.$this->a_table.'."SOL_SPTA" T2 ON T2."DOCNUM" = T0."SOL_SPTA"
			LEFT JOIN '.$this->a_table.'."SOL_PETANI" T3 ON T2."SOL_REGISTRASI"= T3."SOL_REGISTRASI"
			WHERE T2."SOL_SOURCE" = \''.$cpid.'\' AND T1."SOL_ISEMPLACEMENT"=\'N\' 
			AND T2."SOL_DATE" BETWEEN \''.$datefrom.'\' AND \''.$dateto.'\' 
			ORDER BY T0."SOL_DATE" DESC, T0."SOL_TIME" DESC LIMIT 50';
				
		#echo $sql;die;
		$out = (new hanaAdapter)->runcustomquery($sql,$this->getHanaConn());
		
		return $out;
	}
	
}
