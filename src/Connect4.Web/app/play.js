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

    get TurnButtonCss() {

        if (this.board !== undefined && this.board.isNextTurnRed) {
            return 'turn-button-red';
        }
        else if (this.board !== undefined && this.board.isNextTurnYellow) {
            return 'turn-button-yellow';
        }
        else {
            return '';
        }
    }

    get CanPlay() {
        return this.board !== undefined && this.board.isWon === false && this.board.isDraw === false;
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
                    return self.http.fetch('api/board/new/4/6', { method: 'post' })
                        .then(response => response.json())
                        .then(boardDto => self.displayBoard(boardDto));
                } else {
                    self.displayBoard(boardDto);
                }
            });
    }

    newBoard() {
        var self = this;

        return self.http.fetch('api/board/new/4/6', { method: 'post' })
            .then(response => response.json())
            .then(boardDto => self.displayBoard(boardDto));
    }

    displayBoard(boardDto) {
        this.board = {
            numberOfRows: boardDto.numberOfRows,
            numberOfColumns: boardDto.numberOfColumns,
            rows: [],
            turnButtons: [],
            turnButtonCss: '',
            isNextTurnRed: boardDto.isNextTurnRed,
            isNextTurnYellow: boardDto.isNextTurnYellow,
            isWon : false,
            isDraw : false
        };

        for (let rowIndex = 0; rowIndex < boardDto.numberOfRows; rowIndex++) {
            var row = [];
            for (let colIndex = 0; colIndex < boardDto.numberOfColumns; colIndex++) {
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

        if (!this.CanPlay)
            return;

        var turnColor =  this.board.isNextTurnRed ? 'red' : 'yellow';

        return this.http.fetch('api/board/' + turnColor + 'turn/' + column, { method: 'post' })
            .then(response => response.json())
            .then(turn => {
                if (turn.isInvalidTurn === false) {

                    switch (turnColor)
                    {
                        case 'red':
                            self.board.rows[turn.locationRow][turn.locationCol].isRed = true;
                            break;
                        case 'yellow':
                            self.board.rows[turn.locationRow][turn.locationCol].isYellow = true;
                            break;
                    }

                    if (turn.isNextTurnRed) {
                        self.board.isNextTurnRed = true;
                        self.board.isNextTurnYellow = false;
                    }
                    else if (turn.isNextTurnYellow) {
                        self.board.isNextTurnRed = false;
                        self.board.isNextTurnYellow = true;
                    }

                    if (turn.isWinningTurn) {
                        self.board.isWon = true;
                        self.board.rows[turn.locationRow][turn.locationCol].isWin = true;
                    }

                    if (turn.isDraw) {
                        self.board.isDraw = true;
                    }
                }
            });
    }

}
