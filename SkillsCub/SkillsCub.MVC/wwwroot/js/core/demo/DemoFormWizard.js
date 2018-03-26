(function(namespace, $) {
	"use strict";

	var DemoFormWizard = function() {
		// Create reference to this instance
		var o = this;
		// Initialize app when document is ready
		$(document).ready(function() {
			o.initialize();
		});

	};
	var p = DemoFormWizard.prototype;

	// =========================================================================
	// INIT
	// =========================================================================

	p.initialize = function() {
		this._initWizard();
	};

	// =========================================================================
	// WIZARD 1
	// =========================================================================

	p._initWizard = function() {
		var o = this;
		$('#rootwizard').bootstrapWizard({
			onTabShow: function(tab, navigation, index) {
				o._handleTabShow(tab, navigation, index, $('#rootwizard'));
			}
		});
	};

	p._handleTabShow = function(tab, navigation, index, wizard){
		var total = navigation.find('li').length;
		var current = index + 0;
		var percent = (current / (total - 1)) * 100;
		var percentWidth = 100 - (100 / total) + '%';
		
		navigation.find('li').removeClass('done');
		navigation.find('li.active').prevAll().addClass('done');
		
		wizard.find('.progress-bar').css({width: percent + '%'});
		$('.form-wizard-horizontal').find('.progress').css({'width': percentWidth});
	};
	
	// =========================================================================
	// WIZARD 1
	// =========================================================================
	
	p._handleTabShow = function(tab, navigation, index, wizard){
		var total = navigation.find('li').length;
		var current = index + 0;
		var percent = (current / (total - 1)) * 100;
		var percentWidth = 100 - (100 / total) + '%';
		
		navigation.find('li').removeClass('done');
		navigation.find('li.active').prevAll().addClass('done');
		
		wizard.find('.progress-bar').css({width: percent + '%'});
		$('.form-wizard-horizontal').find('.progress').css({'width': percentWidth});
	};
	
	// =========================================================================
	namespace.DemoFormWizard = new DemoFormWizard;
}(this.materialadmin, jQuery)); // pass in (namespace, jQuery):
