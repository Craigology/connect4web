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

  loadBoard() {
    var self = this;

    return this.http.fetch('api/board/get')
      .then(response => response.json())
      .then(boardDto => {
          if (boardDto === null) {
              return self.http.fetch('api/board/new/5/5', { method: 'post' })
                 .then(response => response.json())
                 .then(boardDto => self.board = boardDto);
          } else {
              self.board  = boardDto;
          }
      });
  }

  activate() {

    return this.loadBoard();
  }
}
