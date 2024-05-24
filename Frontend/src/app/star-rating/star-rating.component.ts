import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  template: `
    <div class="star-rating">
      <span *ngFor="let star of stars" (click)="onStarClick(star)" [class.full]="star <= rating" class="gold-star">&#9733;</span>
    </div>
  `,
  styles: [
    `
      .star-rating {
        font-size: 24px;
      }

      span {
        cursor: pointer;
      }

      span.full {
        color: gold;
      }
      
    `,
  ],
})
export class StarRatingComponent {
  @Input() rating: number = 0;
  @Output() ratingChange = new EventEmitter<number>();

  stars: number[] = [1, 2, 3, 4, 5];

  onStarClick(star: number): void {
    this.rating = star;
    this.ratingChange.emit(this.rating);
  }
}

