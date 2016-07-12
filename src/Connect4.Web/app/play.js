import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import 'fetch';

@inject(HttpClient)
export class Play {

  constructor(http) {
    http.configure(config => {
      config
        .useStandardConfiguration();
    });

    this.http = http;
  }

  activate() {
    return this.http.fetch('api/Board/NewBoard/5/5')
      .then(response => response.json());
      //.then(users => this.users = users);
  }
}
