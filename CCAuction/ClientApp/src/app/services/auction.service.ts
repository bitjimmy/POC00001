import { Injectable, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Category } from '../models/category';
import { User } from '../models/user';
import { Item } from '../models/item';

@Injectable()
export class AuctionService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  ngOnInit() { }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'api/categories');
  }

  getMembers() {
    return this.http.get<User[]>(this.baseUrl + 'api/users/members');
  }

  getItems() {
    return this.http.get<Item[]>(this.baseUrl + 'api/items');
  }

  getItemsByCategoryName(name: string) {
    return this.http.get<Item[]>(this.baseUrl + 'api/items/category/' + name);
  }
}
