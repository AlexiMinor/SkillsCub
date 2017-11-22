import 'reflect-metadata';
import 'zone.js';
import 'bootstrap';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.browser.module';
import './assets/css/materialadmin.css';
import './assets/css/font-awesome.min.css';
import './assets/css/material-design-iconic-font.min.css';


import './assets/js/libs/jquery/jquery-1.11.2.min.js';
import './assets/js/libs/jquery/jquery-migrate-1.2.1.min.js';
import './assets/js/libs/bootstrap/bootstrap.min.js';
import './assets/js/libs/spin.js/spin.min.js';
import './assets/js/libs/autosize/jquery.autosize.min.js';
import './assets/js/libs/nanoscroller/jquery.nanoscroller.min.js';
import './assets/js/core/source/App.js';
import './assets/js/core/source/AppNavigation.js';
import './assets/js/core/source/AppOffcanvas.js';
import './assets/js/core/source/AppCard.js';
import './assets/js/core/source/AppForm.js';
import './assets/js/core/source/AppNavSearch.js';
import './assets/js/core/source/AppVendor.js';
import './assets/js/core/demo/Demo.js';
import './assets/js/core/demo/DemoLayouts.js';

if (module.hot) {
    module.hot.accept();
    module.hot.dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector('app');
        const newRootElem = document.createElement('app');
        oldRootElem!.parentNode!.insertBefore(newRootElem, oldRootElem);
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
