import { Component, OnInit, TemplateRef, ContentChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { AuctionService } from '../services/auction.service';
import { Category } from '../models/category';
import { User } from '../models/user';
import { Item } from '../models/item';
import { ItemBidHistory } from '../models/itemBidHistory';

@Component({
  selector: 'app-auction-component',
  styleUrls: ['./auction.component.css'],
  templateUrl: './auction.component.html',
  providers: [AuctionService, DatePipe] // need to include DatePipe in providers list
})
export class AuctionComponent implements OnInit {
  public categories: Category[];
  public users: User[];
  public items: Item[];
  public cacheMobileItems: Item[];
  public selectedCategory: Category;
  public selectedUser: User;
  public selectedItem: Item;
  public err: string;
  public bidMessage: string;
  modalRef: BsModalRef;

  constructor(private auctionService: AuctionService, private modalService: BsModalService, private datePipe: DatePipe) {
  }

  ngOnInit() {
    // initialise data
    this.auctionService.getCategories().subscribe(result => this.categories = result, error => console.error(error));
    this.auctionService.getMembers().subscribe(result => this.users = result, error => console.error(error));
  }

  selectUser(user: User) {
    this.selectedUser = user;
  }

  selectCategory(category: Category) {
    this.selectedCategory = category;
    this.getItemByCategoryName(category.name);
  }

  selectItem(item: Item) {
    this.err = undefined;
    this.selectedItem = item;
  }

  getItemByCategoryName(categoryName: string) {
    this.auctionService.getItemsByCategoryName(categoryName).subscribe(result => {
      this.items = result;

      // additional information of item property
      this.items.forEach((x, index) => {
        if (x.currencyType) {
          switch (x.currencyType) {
            case 2:
              x.currencySymbol = 'AUD$';
              break;
            default:
              x.currencySymbol = 'USD$';
              break;
          }
        } else {
          x.currencySymbol = '$';
        }

        switch (x.conditionType) {
          case 1:
            x.conditionName = 'New';
            break;
          case 2:
            x.conditionName = 'Used';
            break;
        }

        x.sellerFullName = this.users.find(y => y.userName === x.seller.userName).fullName;
        x.index = index;
      });

      // items in category 'Mobile' are used for bid test.
      if (this.cacheMobileItems === undefined && categoryName === 'Mobile') {
        // save items to local cacheMobileItems, so members can bid on the items without refresh the items (unless refresh the page) if category is changed.
        this.cacheMobileItems = this.items;
      } else if (categoryName === 'Mobile') {
        // replace items by local cacheMobileItems to mimic data is being saved in backend
        this.items = this.cacheMobileItems;
      }
    }, error => console.error(error));
  }

  bidItem(item: Item) {
    this.err = undefined;

    // verify bid
    if (this.isBidExpired(item)) {
      this.err = 'Bid is expired.';
    } else if (item.seller.userName === this.selectedUser.userName) {
      this.err = `You cannot bid ${item.name} because you are the seller for this item.`;
    } else if (item.bidPrice === undefined || item.bidPrice === null) {
      this.err = `Please place your bid price for ${item.name}.`;
    } else if (item.bidPrice < item.startPrice) {
      this.err = `Your bid of ${item.currencySymbol} ${item.bidPrice.toFixed(2)} is less than the start price of ${item.currencySymbol} ${item.startPrice.toFixed(2)}.`;
    }

    // if bid is verified, procceed to update the hihest bid price, bid count and add a new entry to bid history
    if (this.err === undefined || this.err === '') {
      if (item.bidPrice > item.highestBidPrice) {
        item.highestBidPrice = parseFloat(item.bidPrice.toFixed(2));
      }

      let now = new Date();
      let newItemBidHistory = new ItemBidHistory(item.id, parseFloat(item.bidPrice.toFixed(2)), now, this.selectedUser.id, this.selectedUser);
      item.itemBidHistories.push(newItemBidHistory);
      item.bidCount += 1;

      // show modal to prompt the member
      let element: HTMLElement = document.getElementById('templateBidButton') as HTMLElement;
      element.click();
    } else {
      // show modal to prompt the member
      let element: HTMLElement = document.getElementById('templateBidErrorButton') as HTMLElement;
      element.click();
    }
  }

  isBidExpired(item: Item) {
    this.bidMessage = undefined;

    // use timestamp comparison and covert end bid date to timestamp
    let now = new Date();
    let nowTimestamp = Date.parse(this.datePipe.transform(now));
    let bidEndDateTimestamp = Date.parse(this.datePipe.transform(item.bidEndDate));

    let isExpired = bidEndDateTimestamp <= nowTimestamp;
    if (isExpired) {
      let highestBidPriceBidder: User;
      item.itemBidHistories.forEach(x => {
        if (x.bidPrice === item.highestBidPrice) {
          highestBidPriceBidder = x.bidder;
        }
      });
      if (highestBidPriceBidder) {
        this.bidMessage = `Bid is expired and item ${item.name} is sold to ${highestBidPriceBidder.userName} for the highest bid price of ${item.currencySymbol} ${item.highestBidPrice.toFixed(2)}.`;
      }
    }
    return isExpired;
  }

  expireBidNow(item: Item) {
    item.bidEndDate = new Date();
    item.durationInDay = 0;
  }

  previousItem(item: Item) {
    this.err = undefined;
    let currentIndex = item.index;
    if (currentIndex > 0) {
      currentIndex -= 1;
      this.selectedItem = this.items[currentIndex];
    }
  }

  nextItem(item: Item) {
    this.err = undefined;
    let currentIndex = item.index;
    if (currentIndex < this.items.length - 1) {
      currentIndex += 1;
      this.selectedItem = this.items[currentIndex];
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}
