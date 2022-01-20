import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject } from 'rxjs';
import { GenericValidator } from './generic-validator';
import { PageRankingDataServiceService } from './page-ranking-data-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Welcome to Page Rank';
  model: FormGroup;
  private validationMessages: { [key: string]: { [key: string]: string } };
  displayMessage$: BehaviorSubject<{ [key: string]: string }> = new BehaviorSubject<{ [key: string]: string }>({});
  displayMessage: { [key: string]: string } = {};
  private genericValidator: GenericValidator;
  disableSearch: boolean = false;
  rankingResult: string = '';

  constructor(formBuilder: FormBuilder, private pageRankService: PageRankingDataServiceService, private snackbar: MatSnackBar) {
    this.model = formBuilder.group({
      url: [null, [Validators.required, Validators.pattern('(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?')]],
      keyboard: ['Online Title Search', [Validators.required]],
      searchEngineType: ['google', [Validators.required]]
    });


    this.validationMessages = {
      url: {
        required: 'URL is required.',
        pattern: 'Please enter a valid URL.'
      },
      keyboard: {
        required: 'Keyboard is required.'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages, false);

    this.displayMessage$.subscribe(m => { this.displayMessage = m });
  }

  ngDoCheck(): void {
    const messages = this.genericValidator.processMessages(this.model);
    this.displayMessage$.next(messages);
  }

  searchRankings() {
    if (this.model.valid) {
      this.disableSearch = true;
      this.pageRankService.getPageRankings(this.model.value.url, this.model.value.keyboard, this.model.value.searchEngineType)
        .subscribe(result => {
          this.disableSearch = false;
          console.log('result',result);
          this.rankingResult = result.join(', ');
        }, (error) => {
          this.snackbar.open('An error has occured while fetching the rankings. Please try again', '', { duration: 5000 });
          this.disableSearch = false;
        });
    }
  }
}
