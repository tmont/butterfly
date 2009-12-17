<?php

	require_once 'PHPUnit/Framework.php';
	
	PHPUnit_Util_Filter::addFileToFilter(__FILE__, 'PHPUNIT');

	$GLOBALS['test_classes'] = array();
	foreach (new DirectoryIterator(dirname(__FILE__)) as $file) {
		if (
			$file->isFile() &&
			strpos($file->getPathName(), DIRECTORY_SEPARATOR . '.') === false &&
			$file->getPathName() !== __FILE__ &&
			substr($file->getFileName(), -9) === 'Tests.php'
		) {
			$testClass = substr($file->getFileName(), 0, -4);
			$GLOBALS['test_classes'][] = $testClass;
			require_once $file->getPathname();
		}
	}
	
	unset($file, $testClass);

	class AllTests {
		
		public static function suite() {
			$suite = new PHPUnit_Framework_TestSuite('All unit tests for Butterfly');
			
			foreach ($GLOBALS['test_classes'] as $class) {
				$suite->addTestSuite($class);
			}
			
			return $suite;
		}
		
	}

?>
