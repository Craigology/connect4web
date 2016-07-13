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

        return this.loadBoard();
    }

    loadBoard() {
        var self = this;

        return this.http.fetch('api/board/get')
            .then(response => response.json())
            .then(boardDto => {
                if (boardDto === null) {
                    return self.http.fetch('api/board/new/5/5', { method: 'post' })
                        .then(response => response.json())
                        .then(boardDto => self.displayBoard(boardDto));
                } else {
                    self.displayBoard(boardDto);
                }
            });
    }

    displayBoard(boardDto) {
        this.board = {
            numberOfRows: boardDto.numberOfRows,
            numberOfColumns: boardDto.numberOfColumns,
            rows: [],
            turnButtons: []
        };

        for (let rowIndex = 0; rowIndex < boardDto.numberOfRows; rowIndex++) {
            var row = [];
            for (let colIndex = 0; colIndex < boardDto.numberOfColumns; colIndex++ ) {
                var index = (rowIndex * boardDto.numberOfColumns) + colIndex;
                row.push(boardDto.locations[index])                
            }
            this.board.rows.push(row);
        }

        for (let turnButtonIndex = 0; turnButtonIndex < boardDto.numberOfColumns; turnButtonIndex++) {
            this.board.turnButtons.push({ column: turnButtonIndex });
        }
    }

    attemptTurn(column) {
        var self = this;

        return this.http.fetch('api/board/yellowturn/' + column, { method: 'post' })
            .then(response => response.json())
            .then(turn => {
                if (turn.isInvalidTurn === false) {
                    self.board.rows[turn.locationRow][turn.locationCol].isYellow = true;
                }
            });
    }
}
