import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';
import moment from 'moment';

export class Auction extends Component {
    displayName = Auction.name;

    constructor(props) {
        super(props);
        this.state = {
            categories: [],
            users: [],
            items: [],
            cacheMobileItems: [],
            selectedCategory: null,
            selectedUser: null,
            selectedItem: null,
            err: "",
            bidMessage: "",
            currentCount: 0
        };

        // This binding is necessary to make "this" work in the callback  
        this.selectUser = this.selectUser.bind(this);
        this.selectCategory = this.selectCategory.bind(this);
        this.selectItem = this.selectItem.bind(this);
        this.expireBidNow = this.expireBidNow.bind(this);
        this.previousItem = this.previousItem.bind(this);
        this.nextItem = this.nextItem.bind(this);

        fetch('api/users/members')
            .then(response => response.json())
            .then(data => {
                this.setState({ users: data });
            });

        fetch('api/categories')
            .then(response => response.json())
            .then(data => {
                this.setState({ categories: data });
            });
    }

    selectUser(user) {
        this.setState({ selectedUser: user },
            //setState takes a callback
            () => console.log(this.state.selectedUser));
    }

    selectCategory(category) {
        this.setState({ selectedCategory: category },
            () => {
                console.log(this.state.selectedCategory);
                fetch('api/items/category/' + this.state.selectedCategory.name)
                    .then(response => response.json())
                    .then(data => {
                        console.log(data);
                        // additional information of item property
                        data.map((x, index) => {
                            if (x.currencyType) {
                                switch (x.currencyType) {
                                    case 2:
                                        x.currencySymbol = 'AUD$';
                                        break;
                                    case 1:
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
                                default:
                                    x.conditionName = 'Used';
                                    break;
                            }

                            x.sellerFullName = this.state.users.find(y => y.userName === x.seller.userName).fullName;
                            x.index = index;
                        });

                        this.setState({ items: data },
                            () => {
                                // items in category 'Mobile' are used for bid test.
                                if (this.state.cacheMobileItems === undefined && this.state.selectedCategory.name === 'Mobile') {
                                    // save items to local cacheMobileItems, so members can bid on the items without refresh the items (unless refresh the page) if category is changed.
                                    this.setState({ cacheMobileItems: this.state.items });

                                } else if (this.state.items === undefined && this.state.selectedCategory.name === 'Mobile') {
                                    // replace items by local cacheMobileItems to mimic data is being saved in backend
                                    this.setState({ items: this.state.cacheMobileItems });
                                }
                            });
                    });
            }
        );
    }

    selectItem(item) {
        this.setState({ selectedItem: item },
            () => console.log(this.state.selectedItem));
    }

    bidItem(item) {
        this.setState({ err: null });

        // verify bid
        var msg = null;
        if (this.isBidExpired(item)) {
            msg = 'Bid is expired.';
        } else if (item.seller.userName === this.state.selectedUser.userName) {
            msg = `You cannot bid ${item.name} because you are the seller for this item.`;
        } else if (item.bidPrice === null) {
            msg = `Please place your bid price for ${item.name}.`;
        } else if (item.bidPrice < item.startPrice) {
            msg = `Your bid of ${item.currencySymbol} ${parseFloat(item.bidPrice).toFixed(2)} is less than the start price of ${item.currencySymbol} ${item.startPrice.toFixed(2)}.`;
        }
        console.log(msg);
        this.setState({ err: msg },
            () => {
                // if bid is verified, procceed to update the hihest bid price, bid count and add a new entry to bid history
                if (this.state.err === null || this.state.err === '') {
                    if (item.bidPrice > item.highestBidPrice) {
                        item.highestBidPrice = parseFloat(item.bidPrice);
                    }

                    var newitemBidHistory = Object.assign({}, item.itemBidHistories[0]);
                    newitemBidHistory.bidDate = new Date();
                    newitemBidHistory.bidPrice = parseFloat(item.bidPrice);
                    item.itemBidHistories.push(newitemBidHistory);
                    item.bidCount += 1;

                    this.setState({ selectedItem: item });
                    console.log(item);
                    // show modal to prompt the member
                    //let element: HTMLElement = document.getElementById('templateBidButton') as HTMLElement;
                    //element.click();
                } else {
                    // show modal to prompt the member
                    // let element: HTMLElement = document.getElementById('templateBidErrorButton') as HTMLElement;
                    //element.click();
                }
            });
    }

    isBidExpired(item) {
        console.log("isBidExpired");
        console.log(item);
        this.setState({ bidMessage: null });

        // use timestamp comparison and covert end bid date to timestamp
        var now = new Date();
        var nowTimestamp = moment(now).format("X");
        var bidEndDateTimestamp = moment(item.bidEndDate).format("X");

        var isExpired = bidEndDateTimestamp <= nowTimestamp;
        if (isExpired) {
            var highestBidPriceBidder;
            for (var i = 0; i < item.itemBidHistories.length; i++) {
                console.log(i);
                console.log(item.itemBidHistories[i].bidPrice);
                if (item.itemBidHistories[i].bidPrice === item.highestBidPrice) {
                    highestBidPriceBidder = item.itemBidHistories[i].bidder;
                }
            }
            console.log(highestBidPriceBidder);
            if (highestBidPriceBidder) {
                var msg = `Bid is expired and item ${item.name} is sold to ${highestBidPriceBidder.userName} for the highest bid price of ${item.currencySymbol} ${parseFloat(item.highestBidPrice).toFixed(2)}.`;
                this.setState({ bidMessage: msg },
                    () => console.log(this.state.bidMessage));
            }
        }
        console.log(isExpired);
        return isExpired;
    }

    expireBidNow(item) {
        item.bidEndDate = new Date();
        item.durationInDay = 0;

        this.setState({ selectedItem: item },
            () => {
                console.log(this.state.selectedItem);
                this.isBidExpired(item);
            });
    }

    previousItem(item) {
        this.setState({ err: null });
        let currentIndex = item.index;
        if (currentIndex > 0) {
            currentIndex -= 1;
            this.setState({ selectedItem: this.state.items[currentIndex] });
        }
    }

    nextItem(item) {
        this.setState({ err: null });
        let currentIndex = item.index;
        if (currentIndex < this.state.items.length - 1) {
            currentIndex += 1;
            this.setState({ selectedItem: this.state.items[currentIndex] });
        }
    }

    handleBidPriceChange(e) {
        this.setState({
            selectedItem: {
                ...this.state.selectedItem,
                bidPrice: e.target.value
            }
        });
        console.log(this.state.selectedItem);
    }

    render() {
        return (
            <div>
                <h1>Auction Website - React</h1>
                <h2>Select a test member to bid</h2>
                <div id="parentUser" className="btn-group btn-group-lg" role="group">
                    {this.state.users.map(user =>
                        <button type="button" className="btn btn-default" key={user.id} onClick={() => this.selectUser(user)}>{user.fullName}</button>
                    )}
                </div>

                {this.state.selectedUser && (
                    <div id="parentCategory">
                        <h2>{this.state.selectedUser.fullName} - Shop by Category</h2>
                        <div className="btn-group btn-group-lg" role="group">
                            {this.state.categories.map(category =>
                                <button type="button" className="btn btn-default" key={category.id} onClick={() => this.selectCategory(category)}>{category.name}</button>
                            )}
                        </div>
                    </div>
                )
                }

                {this.state.selectedCategory && (
                    <div id="parentItem">
                        <h2>{this.state.selectedCategory.name} - Shop by Item</h2>
                        {this.state.items.length > 0 && (
                            <div id="parentItemOption">
                                <div className="btn-group btn-group-lg" role="group">
                                    {this.state.items.map(item =>
                                        <button type="button" className="btn btn-default" key={item.id} onClick={() => this.selectItem(item)}>{item.name}</button>
                                    )}
                                </div>
                                {this.state.selectedItem && (
                                    <div id="parentItemProfolio">
                                        {(
                                                <div className="alert alert-danger some-gap" role="alert">{this.state.bidMessage}</div>
                                        )}
                                        <h3>Bid your {this.state.selectedItem.name}</h3>
                                        <div><label>ID: {this.state.selectedItem.id}</label></div>
                                        <div><label>Name: {this.state.selectedItem.name}</label></div>
                                        <div><label>Description: {this.state.selectedItem.description}</label></div>
                                        <div><label>Condition: {this.state.selectedItem.conditionName}</label></div>
                                        <div><label>Seller: {this.state.selectedItem.sellerFullName}</label></div>
                                        <div><label>Start Price: {this.state.selectedItem.currencySymbol} {this.state.selectedItem.startPrice.toFixed(2)}</label></div>
                                        <div><label>Current Highest Bid Price: {this.state.selectedItem.currencySymbol} {this.state.selectedItem.highestBidPrice.toFixed(2)}</label></div>
                                        <div>
                                            <label>Bid End Date: <Moment format="YYYY-MM-DD hh:mm a">{this.state.selectedItem.bidEndDate}</Moment></label>
                                            {(
                                                <button type="button" className="btn btn-default" onClick={() => this.expireBidNow(this.state.selectedItem)}>Expire bid now for testing</button>
                                            )}
                                        </div>
                                        <div><label>Remaining Duration: {this.state.selectedItem.durationInDay} Days</label></div>
                                        <div><label>Bid Count: {this.state.selectedItem.bidCount}</label></div>
                                        <div>
                                            <label>
                                                Place Your Bid Price: {this.state.selectedItem.currencySymbol}
                                                <input placeholder="Bid Price" type="number" step="0.01" min="0" max="9999" className="input-lg" value={this.state.selectedItem.bidPrice} onChange={this.handleBidPriceChange.bind(this)} />
                                            </label>
                                        </div>
                                        <button type="button" className="btn btn-default btn-lg" onClick={() => this.previousItem(this.state.selectedItem)}>&lt;&lt;</button>
                                        <button type="button" className="btn btn-default btn-lg" onClick={() => this.nextItem(this.state.selectedItem)}>&gt;&gt;</button>
                                        {(
                                            <button onChange={this.handleBidPriceChange} type="button" className="btn btn-default btn-lg" onClick={() => this.bidItem(this.state.selectedItem)}>Bid {this.state.selectedItem.name}
                                                {this.state.selectedItem.bidPrice && (
                                                    <div>
                                                        for {this.state.selectedItem.currencySymbol}  {this.state.selectedItem.bidPrice}
                                                    </div>
                                                )}
                                            </button>
                                        )}
                                    </div>
                                )}
                            </div>
                        )}
                        {this.state.items.length === 0 && (
                            <h3>Sorry...there are no items found in {this.state.selectedCategory.name} Category.</h3>
                        )}
                    </div>
                )
                }
            </div>
        );
    }
}
  
