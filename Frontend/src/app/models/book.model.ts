export interface Book {
    id: number;
    name: string;
    author: string;
    genre: string;
    description: string;
    isBookAvailable:boolean;
    lentByUserId:number;
    borrowUserId:number;
  }
  
