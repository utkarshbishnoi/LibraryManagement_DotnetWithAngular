import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAddedBooksComponent } from './my-added-books.component';

describe('MyAddedBooksComponent', () => {
  let component: MyAddedBooksComponent;
  let fixture: ComponentFixture<MyAddedBooksComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAddedBooksComponent]
    });
    fixture = TestBed.createComponent(MyAddedBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
