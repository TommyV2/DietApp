import React from "react";
import {AddProductPopUp} from "./AddProductPopUp";


export class ProductList extends React.Component {
  state = {
    seen: false
  };

  togglePop = () => {
    this.setState({
      seen: !this.state.seen
    });
  };

  render() {
    return (
      <div>
        <div className="btn" onClick={this.togglePop}>
          <button>Dodaj produkt</button>
        </div>
        {this.state.seen ? <AddProductPopUp ownerId="1" toggle={this.togglePop} /> : null} 
      </div>
    );
  }
}