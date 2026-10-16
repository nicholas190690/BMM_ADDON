<?php
namespace Zircon\Modules\Checkpointservices\Models;
/*
 * Bismillahirrahmanirrahim...									Blitar, 25 of September 2019 
 * This model is created for SptaRestApi Controller 
 */
 #use Zircon\hanaAdapter as hanaAdapter;
 use Zircon\hanaAdapterv2 as hanaAdapter;
 
 class General {
	
	private $a_table;
	private $c_table; 
	
	public function __construct(){
		#TEST_TABLE 
		$this->a_table	= 'RMI_ADDON_MASTER';
		#$this->a_table	= 'UAT_ADDON2';
		
		$this->c_table	= 'RMI';
		#$this->c_table	= 'UAT2';
	}
	
	/*
	 * Getting hanna adapter 
	 */
	public function getHanaConn(){
		$hdbodbc = new hanaAdapter();
		$conn = $hdbodbc->hdbconnect("192.168.1.19:30015","","SOLTIUS","P@ssw0rd");
		
		return $conn;
	}
	
	public function runCustomQuery($SQL){
		$adapter	=	new hanaAdapter(); 
		$hcon		=	$this->getHanaConn();	
		$recs		=	$adapter->runcustomquery($SQL,$hcon);
		
		return $recs;
	}
 }	
