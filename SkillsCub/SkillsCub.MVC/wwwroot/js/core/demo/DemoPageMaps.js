(function (namespace, $) {
	"use strict";

	var DemoPageMaps = function () {
		// Create reference to this instance
		var o = this;
		// Initialize app when document is ready
		$(document).ready(function () {
			o.initialize();
		});

	};
	var p = DemoPageMaps.prototype;

	// =========================================================================
	// MEMBERS
	// =========================================================================

	p.map = null;

	// =========================================================================
	// INIT
	// =========================================================================

	p.initialize = function () {
		this._initMarkerMap();
	};

	// =========================================================================
	// MARKER MAP
	// =========================================================================

	p._initMarkerMap = function () {
		if (typeof GMaps === 'undefined') {
			return;
		}
		if ($('#marker-map').length === 0) {
			return;
		}

		var markerMap = new GMaps({
			div: '#marker-map',
			lat: 53.9105462,
			lng: 27.5297105,
			zoom: 17
		});

		markerMap.addMarker({
            lat: 53.9105462,
            lng: 27.5297105
		});
	};
    
	namespace.DemoPageMaps = new DemoPageMaps;
}(this.materialadmin, jQuery)); // pass in (namespace, jQuery):
