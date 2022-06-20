import { Component, OnInit } from '@angular/core';
import { PaymentsenseCodingChallengeApiService } from '../services';
import { take } from 'rxjs/operators';

import { faThumbsUp, faThumbsDown } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.scss']
})

export class HealthCheckComponent implements OnInit {
  public faThumbsUp = faThumbsUp;
  public faThumbsDown = faThumbsDown;
  public title = 'Paymentsense Coding Challenge!';
  public paymentsenseCodingChallengeApiIsActive = false;
  public paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
  public paymentsenseCodingChallengeApiActiveIconColour = 'red';

  constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {
    paymentsenseCodingChallengeApiService.getHealth().pipe(take(1))
    .subscribe(
      apiHealth => {
        this.paymentsenseCodingChallengeApiIsActive = apiHealth === 'Healthy';
        this.paymentsenseCodingChallengeApiActiveIcon = this.paymentsenseCodingChallengeApiIsActive
          ? this.faThumbsUp
          : this.faThumbsUp;
        this.paymentsenseCodingChallengeApiActiveIconColour = this.paymentsenseCodingChallengeApiIsActive
          ? 'green'
          : 'red';
      },
      _ => {
        this.paymentsenseCodingChallengeApiIsActive = false;
        this.paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
        this.paymentsenseCodingChallengeApiActiveIconColour = 'red';
      });
   }

  ngOnInit() {
  }

}
