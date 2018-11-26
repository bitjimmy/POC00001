import { ItemBidHistory } from '../models/itemBidHistory';
import { User } from '../models/user';

export class Item {
  id: string;
  categoryId: string;
  name: string;
  description: string;
  conditionType: number;
  currencyType: number;
  startPrice: number;
  highestBidPrice: number;
  bidStartDate: Date;
  bidEndDate: Date;
  sellerId: string;
  seller: User;
  durationInDay: number;
  bidCount: number;
  itemBidHistories: ItemBidHistory[];
  bidPrice: number;
  currencySymbol: string;
  conditionName: string;
  sellerFullName: string;
  index: number;
}
