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
    this.board = undefined;
  }

  activate() {
    //     return this.http.fetch('api/board/new', { method: 'post' })
    //   .then(response => response.json());
    //   //.then(users => this.users = users);

    this.board = this.http.fetch('api/board/get')
      .then(response => response.json());
      //.then(users => this.users = users);
  }
}
