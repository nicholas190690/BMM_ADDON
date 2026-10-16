<?php
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use stdClass;
use Zircon\hanaAdapter as hanaAdapter;

class OUuser extends Model
{
	private $fetch;
	private $conn;
	public $a_table;
	
	public function initialize($fetch=[])
	{
		$this->fetch=$fetch;
		
		#$this->a_table="UAT_ADDON2";
		$this->a_table	= 'RMI_ADDON_MASTER';
	}
	
	/*
	 * Getting hana connection using odbc
	 */
	private function getHanaConn(){
		$hdbodbc = new hanaAdapter();
		$this->conn = $hdbodbc->hdbconnect("192.168.1.19:30015","","SYSTEM","P@ssw0rd");
		
		return $this->conn;
	}
	
	/*
	 * Updated 14/07/2020 08:20 added T1."ISCANEYARD" line
	 *
	 */
	 
	public function getUser($user)
	{
		if(!empty($user)||$user!=null)
		{
			$sql='SELECT 
				T0."UNAME" As "Username",
				T0."UFNAME" As "FirstName",
				T0."UMNAME" As "MiddleName",
				T0."ULNAME" As "LastName",
				T0."UPASS",
				T0."UEMAIL" As "Email",
				T0."UCONTACTNR" As "Contact",
				T0."UVPOS",
				T0."UAPOS",
				T0."UKDPOSPANTAU" As "KdPosPantau",
				T1."NAME" As "NmPosPantau",
				T1."RMI_LATITUDE" As "Latitude",
				T1."RMI_LONGTITUDE" As "Longitude",
				T1."RMI_RADIUS" As "Radius",
				T0."UATAP" As "TapPerm",
				T0."UVPSPTA" As "PrintSPTAPerm",
				T1."SOL_ISEMPLACEMENT" AS "Emp",
				T1."ISCANEYARD" AS "Caneyard",
				T0."IS_LOGIN",
				T0."UAACTIVE" As "active"
				FROM '.$this->a_table.'."SOL_USER" T0 
				LEFT JOIN '.$this->a_table.'."SOL_POSPANTAU" T1 ON T0."UKDPOSPANTAU"=T1."CODE" WHERE T0."UNAME" = \''.$user.'\' ';
				
			$records=(new hanaAdapter)->runcustomquery($sql,$this->getHanaConn());
			
			if(count($records)>0){
				$this->fetch=$records[0];
			}else{
				$this->fetch=$records;
			}
		}else{
			if(empty($user))
			{
				$this->fetch="user is empty";
			}else if($user==null){
				$this->fetch="its null";
			}
		}
		
		return $this->fetch;
	}
	
	public function updateIsLogin($user, $status)
	{
		if(!empty($user) && $user!=null)
		{
			$sql1 = 'UPDATE "RMI_ADDON_MASTER"."SOL_USER" T0
			SET 
			T0."IS_LOGIN"=\''.$status.'\'
			WHERE T0."UNAME"=\''.$user.'\'';
			//echo $sql1;
				
			$records=(new hanaAdapter)->runcustomquery($sql1,$this->getHanaConn());
			
			if($records){
				$this->fetch= "success";
			}else{
				$this->fetch= "false";
			}
		}else{
			if(empty($user))
			{
				$this->fetch="user is empty";
			}else if($user==null){
				$this->fetch="its null";
			}
		}
		
		return $this->fetch;
	}
}
