<?php

	require_once 'PHPUnit/Framework.php';
	require_once dirname(dirname(__FILE__)) . '/Butterfly.php';
	
	PHPUnit_Util_Filter::addFileToFilter(__FILE__, 'PHPUNIT');

	abstract class ButterflyTestCase extends PHPUnit_Framework_TestCase {
		
		protected $butterfly;
		
		public function setUp() {
			$this->butterfly = new Butterfly();
		}
		
		public function tearDown() {
			$this->butterfly = null;
		}
	
	}

?>