import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import 'fetch';

@inject(HttpClient)
export class App {

  constructor(http) {
    http.configure(config => {
      config
        .useStandardConfiguration();
    });

    this.http = http;
  }

  configureRouter(config, router) {
    config.title = 'Connect4';
    config.map([
      { route: ['', 'landing'], name: 'landing',    moduleId: 'landing',   nav: true, title: 'Home' },
      { route: 'play',          name: 'play',       moduleId: 'play',      nav: true, title: 'Play' }
    ]);

    this.router = router;
  }
}
