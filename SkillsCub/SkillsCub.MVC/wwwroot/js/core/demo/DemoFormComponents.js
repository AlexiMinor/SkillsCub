(function (namespace, $) {
	"use strict";

	var DemoFormComponents = function () {
		// Create reference to this instance
		var o = this;
		// Initialize app when document is ready
		$(document).ready(function () {
			o.initialize();
		});

	};
	var p = DemoFormComponents.prototype;

	// =========================================================================
	// INIT
	// =========================================================================

	p.initialize = function () {
		this._initTypeahead();
		this._initAutocomplete();
		this._initSelect2();
		this._initMultiSelect();
		this._initInputMask();
		this._initDatePicker();
	};

	// =========================================================================
	// TYPEAHEAD
	// =========================================================================

	p._initTypeahead = function () {
		if (!$.isFunction($.fn.typeahead)) {
			return;
		}
		var countries = new Bloodhound({
			datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
			queryTokenizer: Bloodhound.tokenizers.whitespace,
			limit: 10,
			prefetch: {
				// url points to a json file that contains an array of country names, see
				// https://github.com/twitter/typeahead.js/blob/gh-pages/data/countries.json
				url: $('#autocomplete1').data('source'),
				// the json file contains an array of strings, but the Bloodhound
				// suggestion engine expects JavaScript objects so this converts all of
				// those strings
				filter: function (list) {
					return $.map(list, function (country) {
						return {name: country};
					});
				}
			}
		});
		countries.initialize();
		$('#autocomplete1').typeahead(null, {
			name: 'countries',
			displayKey: 'name',
			// `ttAdapter` wraps the suggestion engine in an adapter that
			// is compatible with the typeahead jQuery plugin
			source: countries.ttAdapter()
		});
	};
	
	// =========================================================================
	// AUTOCOMPLETE
	// =========================================================================

	p._initAutocomplete = function () {
		if (!$.isFunction($.fn.autocomplete)) {
			return;
		}

		$.ajax({
			url: $('#autocomplete2').data('source'),
			dataType: "json",
			success: function (countries) {
				$("#autocomplete2").autocomplete({
					source: function (request, response) {
						var results = $.ui.autocomplete.filter(countries, request.term);
						response(results.slice(0, 10));
					}
				});
			}
		});
	};

	// =========================================================================
	// SELECT2
	// =========================================================================

	p._initSelect2 = function () {
		if (!$.isFunction($.fn.select2)) {
			return;
		}
		$(".select2-list").select2({
			allowClear: true
		});
	};

	// =========================================================================
	// MultiSelect
	// =========================================================================

	p._initMultiSelect = function () {
		if (!$.isFunction($.fn.multiSelect)) {
			return;
		}
		$('#optgroup').multiSelect({selectableOptgroup: true});
	};

	// =========================================================================
	// InputMask
	// =========================================================================

	p._initInputMask = function () {
		if (!$.isFunction($.fn.inputmask)) {
			return;
		}
		$(".form-control.time-mask").inputmask('h:s', {placeholder: 'hh:mm'});
	};

	// =========================================================================
	// Date Picker
	// =========================================================================

	p._initDatePicker = function () {
		if (!$.isFunction($.fn.datepicker)) {
			return;
		}
		$('#start-date').datepicker({todayHighlight: true});
        $('#consult-date').datepicker({ todayHighlight: true });
	    $('#demo-date-range').datepicker({ todayHighlight: true });
	};

	// =========================================================================
	// DATATABLES
	// =========================================================================

	p.initDataTables = function (grid) {
		if (!$.isFunction($.fn.dataTable)) {
			return;
		}

		$.extend(jQuery.fn.dataTableExt.oSort, {
			"numeric-comma-pre": function (a) {
				var x = (a == "-") ? 0 : a.replace(/,/, ".");
				return parseFloat(x);
			},
			"numeric-comma-asc": function (a, b) {
				return ((a < b) ? -1 : ((a > b) ? 1 : 0));
			},
			"numeric-comma-desc": function (a, b) {
				return ((a < b) ? 1 : ((a > b) ? -1 : 0));
			}
		});
		grid.dataTable({
			"sDom": 'lCfrtip',
			"sPaginationType": "full_numbers",
			"aaSorting": [],
			"aoColumns": [
				null,
				null,
				null,
				{"sType": "numeric-comma"},
				null
			],
			"oColVis": {
				"buttonText": "Columns",
				"iOverlayFade": 0,
				"sAlign": "right"
			},
			"oLanguage": {
				"sLengthMenu": '_MENU_ entries per page',
				"sSearch": '<i class="icon-search"></i>',
				"oPaginate": {
					"sPrevious": '<i class="fa fa-angle-left"></i>',
					"sNext": '<i class="fa fa-angle-right"></i>'
				}
			}
		});
	};

	// =========================================================================
	namespace.DemoFormComponents = new DemoFormComponents;
}(this.materialadmin, jQuery)); // pass in (namespace, jQuery):
