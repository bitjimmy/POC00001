import { User } from '../models/user';
export class ItemBidHistory {
  id: string;
  itemId: string;
  bidPrice: number;
  bidDate: Date;
  bidderId: string;
  bidder: User;

  constructor (newItemId : string, newBidPrice : number, newBidDate: Date, newBidderId: string, newBidder: User) {
    this.itemId = newItemId;
    this.bidPrice = newBidPrice;
    this.bidDate = newBidDate;
    this.bidderId = newBidderId;
    this.bidder = newBidder;
  }
}
