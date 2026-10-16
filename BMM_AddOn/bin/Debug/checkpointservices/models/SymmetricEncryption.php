<?php
namespace Zircon\Modules\Checkpointservices\Models;

use Phalcon\Mvc\Model;
use stdClass;

class SymmetricEncryption extends Model 
{
	public $cipher='aes-256-cbc';
	
	public function initialize($cipher='aes-256-cbc')
	{
		//$cipher='aes-256-cbc';
		$this->cipher=$cipher;
	}
	
	private function getKeySize()
	{
        if(preg_match("/([0-9]+)/i",$this->cipher,$matches))
		{
            return $matches[1] >> 3;
        }
        return 0;
    }

    private function derived($password,$salt)
	{
        $AESKeyLength=$this->getKeySize();
		$this->cipher='aes-256-cbc';
        $AESIVLength=openssl_cipher_iv_length($this->cipher);

        $pbkdf2=hash_pbkdf2("SHA1",$password,mb_convert_encoding($salt,'UTF-16LE'),1000,$AESKeyLength+$AESIVLength,TRUE);

        $key=substr($pbkdf2,0,$AESKeyLength);
        $iv=substr($pbkdf2,$AESKeyLength,$AESIVLength);

        $derived=new stdClass();
        $derived->key=$key;
        $derived->iv=$iv;
        return $derived;
    }

    function encrypt($message="",$password,$salt)
	{
        $derived=$this->derived($password, $salt);
        $enc=openssl_encrypt(mb_convert_encoding($message,'UTF-16','UTF-8'),$this->cipher,$derived->key,NULL,$derived->iv);
        return $enc;
    }

    function decrypt($message="",$password,$salt)
	{
        $derived=$this->derived($password,$salt);
        $dec=openssl_decrypt($message,$this->cipher,$derived->key,NULL,$derived->iv);
        //return mb_convert_encoding($dec,'UTF-8','UTF-16LE');
		return utf8_decode($dec);
    }
	
	function decryptText($input,$password){
		$tempAry = str_replace("-", "", $input);

		$bytesToBeDecryptedbin = hex2bin($tempAry); 
		$bytesToBeDecrypted = unpack('C*', $bytesToBeDecryptedbin);
		$bytesToBeDecryptedbinstring = "";
		for($i=0;$i<count($bytesToBeDecrypted);$i++){
			$bytesToBeDecryptedbinstring=$bytesToBeDecryptedbinstring.chr($bytesToBeDecrypted[$i+1]);
		}


		$passwordhash = hash('sha256', $password); 
		$passwordbin = hex2bin($passwordhash); 
		$passwordBytes = unpack('C*', $passwordbin);
		$passwordBytesstring = "";
		for($i=0;$i<count($passwordBytes);$i++){
			$passwordBytesstring=$passwordBytesstring.chr($passwordBytes[$i+1]);
		}

		$saltBytes = array(5,7,3,5,2,6,7,8);
		$saltBytesstring = "";
		for($i=0;$i<count($saltBytes);$i++){
			$saltBytesstring=$saltBytesstring.chr($saltBytes[$i]);
		}
		
		$keySize = 256; $blockSize = 128;

		$key = hash_pbkdf2("sha1", $passwordBytesstring, $saltBytesstring, 1000, 48, true); 
		$aeskey = (  substr($key,0,$keySize/8) );
		$aesiv =  (  substr($key,$keySize/8,$blockSize/8) );

		$decrypted = mcrypt_decrypt
			  (
				  MCRYPT_RIJNDAEL_128,
				  $aeskey,
				  $bytesToBeDecryptedbinstring,
				  MCRYPT_MODE_CBC,
				  $aesiv
			   );        

		$result = "";
		for($i=0;$i<strlen($decrypted);$i++){
			if($decrypted[$i]==chr(7)) break;
			$result = $result . $decrypted[$i];
		}
			   
		$decryptedarr = unpack('C*', $result);
		return $result;
	}
	
	/*
	function jangkrikbossreborn($sText,$sKey)
	{
		$saltBytes=[0x49,0x76,0x61,0x6E,0x20,0x4D,0x65,0x64,0x76,0x65,0x64,0x65,0x76];
		$saltBytesstring="";
		$pwdutf16=mb_convert_encoding(strtoupper($sText),'utf-16le');
		$passwordBytesstring=base64_decode($sText,true);
		$decryptedBytes=null;
		
		for($i=0;$i<count($saltBytes);$i++){ 
			//echo $i;
			$saltBytesstring=$saltBytesstring.chr($saltBytes[$i]);
		}
		$AESKeyLength=256/8;
		$AESIVLength=128/8;
				
		$key=hash_pbkdf2("sha1",base64_encode($sKey),base64_encode($saltBytesstring),10000,$AESKeyLength+$AESIVLength, true);
		$aeskey=(substr($key,0,$AESKeyLength));
		$aesiv=(substr($key,$AESKeyLength,$AESIVLength));
		$decrypted = mcrypt_decrypt(
			MCRYPT_RIJNDAEL_128,
			$aeskey,
			$passwordBytesstring,
			MCRYPT_MODE_CBC,
			$aesiv);
				
		$arr=str_split($decrypted);
		for($i=0;$i<count($arr);$i++){
			$arr[$i]=ord($arr[$i]);
		}
		$decryptedBytes=$arr;
		$decryptedString="";
		
		for($i=0;$i<count($decryptedBytes);$i++){ 
			$decryptedString=$decryptedString.chr($decryptedBytes[$i]);
		}
		
		//$retval=call_user_func_array("pack", array_merge(array("C*"), $decryptedBytes));
		
		return utf8_decode($decryptedString);
	}*/
	
	function encryptreborn($sText,$sKey)
	{
		$RetVal="";
		$EncryptionKey=$sKey;
		
		$password=mb_convert_encoding($sText,'utf-16le');
		
		$clearBytes=unpack('C*',$password);
		
		$clearBytesHexArr=[];
		for($i=0;$i<count($clearBytes);$i++)
		{
			$clearBytesHexArr[$i]=dechex($clearBytes[$i+1]);
		}
		
		$clearBytesString="";
		
		for($i=0;$i<count($clearBytes);$i++)
		{
			$clearBytesString.=$clearBytesHexArr[$i];
		}
		
		$clearBytesString64=base64_encode($clearBytesString);
		
		$saltBytes=[
			0x49,0x76,0x61,0x6E,
			0x20,0x4D,0x65,0x64,
			0x76,0x65,0x64,0x65,
			0x76];
		$saltBytesstring="";
		for($i=0;$i<count($saltBytes);$i++){
			$saltBytesstring=$saltBytesstring.chr($saltBytes[$i]);
		}
		$encryptMethod='AES-256-CBC';
		
		$ivLength = openssl_cipher_iv_length($encryptMethod);
        $iv = openssl_random_pseudo_bytes($ivLength);
		
		$AESKeyLength=256/4;
		$AESIVLength=128/4;
		$pbkdf2=hash_pbkdf2("sha1",$EncryptionKey,
			$saltBytesstring,
			1000,$AESKeyLength+$AESIVLength ,FALSE);
		
		$key=mb_substr($pbkdf2,0,$AESKeyLength);
		//$iv=mb_substr($pbkdf2,$AESKeyLength,$AESIVLength);
		
		$xkey=$key;
		$xiv=$iv;
		
		$encrypted=openssl_encrypt(
			$sText,
			$encryptMethod,
			hex2bin($pbkdf2),
			$options=OPENSSL_RAW_DATA,
			$xiv);
		
		$hmac = hash_hmac('sha1', $encrypted, $xkey, $as_binary=true);
		
		return $encrypted;
	}
	
	function decryptreborn($sText,$sKey)
	{
		$RetVal="";
		$EncryptionKey=$sKey;
		
		$saltBytes=[
			0x49,0x76,0x61,0x6E,
			0x20,0x4D,0x65,0x64,
			0x76,0x65,0x64,0x65,
			0x76];
		$saltBytesstring="";
		for($i=0;$i<count($saltBytes);$i++){
			$saltBytesstring=$saltBytesstring.chr($saltBytes[$i]);
		}		
		
		$AESKeyLength=256/4;
		$AESIVLength=128/4;
		$pbkdf2=hash_pbkdf2("sha1",$EncryptionKey,
			$saltBytesstring,
			1000,$AESKeyLength+$AESIVLength ,FALSE);
		
		$key=mb_substr($pbkdf2,0,$AESKeyLength);
		$iv=mb_substr($pbkdf2,$AESKeyLength,$AESIVLength);
		
		$xkey=hex2bin($key);
		$xiv=hex2bin($iv);
		
		$decrypted=openssl_decrypt(
			base64_decode($sText),
			'AES-256-CBC',
			$xkey,
			OPENSSL_RAW_DATA,
			$xiv,
			FALSE);
		
		$arr=str_split($decrypted);
		for($i=0;$i<count($arr);$i++){
			$arr[$i]=ord($arr[$i]);
		}
		$decryptedBytes=$arr;
		$arr=[];
		$decryptedString="";
		$result="";
		for($i=0;$i<count($decryptedBytes);$i++){
			if($decryptedBytes[$i]!=0){
				$result=$result.chr($decryptedBytes[$i]);
			}
		}		
		$RetVal=$result;
		
		return $RetVal;
	}
	
}
