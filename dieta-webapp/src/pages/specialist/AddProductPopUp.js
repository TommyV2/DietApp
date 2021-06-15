import React from "react";
import "./AddProductPopUp.css";
import { url } from "../../commons/properties"
import { Row, Col } from "react-bootstrap";
import { connect } from "react-redux";

export class AddProductPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            ownerId: this.props.ownerId,
            productId: this.props.productId,
            nutrients: [],
            product: null,
            msg: ""
        };
    }


    handleClick = () => {
        this.props.toggle();
    };

    handleNutrient(id, event, nutr){
        
        //let nutrients = this.state.nutrients;
        let nutrients = nutr;
        let found = false;  
        const amount = event.target.value;
        console.log(amount)
        for(let idx in nutrients){
            if(nutrients[idx].id === id){
                let nutrient = { 
                    nutrientId: id,
                    amount: amount
                };
                nutrients[idx] = nutrient;
                found = true;
                break; 
            }
        }
        if (found === false){
            let nutrient = { 
                nutrientId: id,
                amount: amount
            };
            nutrients.push(nutrient);
        }
        this.setState({nutrients: nutrients});
        console.log(this.state.nutrients)
    }

    success() {
        console.log("Dodano produkt!")     
    }

    error(err) {
        console.log('Nie dodano produktu:', err);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        const ownerId = +this.state.ownerId
        const source = "USER"; // tutaj można zmienić na this.props.source jak dodamy dodawanie produktów z zewnątrz
        const name = event.target.name.value;
        const kcal = +event.target.kcal.value;
        const carbohydrates = (event.target.carbohydrates.value) ? parseFloat(event.target.carbohydrates.value) : 0;
        const sugar = (event.target.sugar.value) ? parseFloat(event.target.sugar.value) : 0;
        const fat = (event.target.fat.value) ? parseFloat(event.target.fat.value) : 0;
        const saturatedFat = (event.target.saturatedFat.value) ? parseFloat(event.target.saturatedFat.value) : 0;
        const protein = (event.target.protein.value) ? parseFloat(event.target.protein.value) : 0;
        const fiber = (event.target.fiber.value) ? parseFloat(event.target.fiber.value) : 0;
        const nutrients = this.state.nutrients;
        const nutrientTest = {
            nutrientId: 1,
            amount: 0
        };
       // nutrients.push(nutrientTest);


        const data = JSON.stringify({
            ownerId: ownerId,
            source: source,
            name: name,
            kcal: kcal,
            carbohydrates: carbohydrates,
            sugar: sugar,
            fat: fat,
            saturatedFat: saturatedFat,
            protein: protein,
            fiber: fiber,
            nutrients: nutrients
        });

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('POST', url + '/product', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano produkt"
        })
            
        
        //this.props.toggle();

    };

    async componentDidMount() {
        const response = await fetch(url + '/nutrients');
        const availableNutrients = await response.json();
        this.props.dispatch({ type: "LOAD_NUTRIENTS", availableNutrients: availableNutrients });
        let productId = this.state.productId;
        let product = null;
        if (productId){
           const response2 = await fetch(url + '/product/'+productId);
        product = await response2.json(); 
        } 
        this.setState({ product: product });

    }

    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
        const availableNutrients = this.props.availableNutrients;
        let newProduct = false;
        let product = this.state.product;
        if(product === null){
            newProduct = true;
            product = {
                ownerId: 1,
                source: "USER",
                name: "",
                kcal: "",
                carbohydrates: "",
                sugar: "",
                fat: "",
                saturatedFat: "",
                protein: "",
                fiber: "",
                nutrients: null
            };
        }
        let nutrients = product.nutrients;
        return (
            <div /*className="modal" */>
                <div className="modal_content">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    <form onSubmit={this.handleSubmit}>
                        <h3>Dodaj produkt:</h3>
                        <Row >
                            <Col>
                                <label>
                                    Nazwa:
              <input type="text" name="name" required defaultValue={product.name}/>
                                </label>
                                <label>
                                    Kalorie:
              <input type="number" name="kcal" min="0"  defaultValue={product.kcal} step="0.01"/>             
                                </label>
                                <label className="wrapper">
                                    Węglowodany:
              <input type="number" name="carbohydrates" min="0"  defaultValue={product.carbohydrates} step="0.01"/>
              <span className="units">g</span>
                                </label>
                                <label className="wrapper">
                                    Tłuszcz:
              <input type="number" name="fat" min="0"  defaultValue={product.fat} step="0.01"/>
              <span className="units">g</span>
                                </label>
                                <label className="wrapper">
                                    Białko:
              <input type="number" name="protein" min="0"  defaultValue={product.protein} step="0.01"/>
              <span className="units">g</span>
                                </label>
                                <label className="wrapper">
                                    Cukier:
              <input type="number" name="sugar" min="0"  defaultValue={product.sugar} step="0.01"/>
              <span className="units">g</span>
                                </label>
                                <label className="wrapper">
                                    Tłuszcz nasycony:
              <input type="number" name="saturatedFat" min="0"  defaultValue={product.saturatedFat} step="0.01"/>
              <span className="units">g</span>
                                </label>
                                <label className="wrapper">
                                    Błonnik:
              <input type="number" name="fiber" min="0" defaultValue={product.fiber} step="0.01"/>
              <span className="units">g</span>
                                </label>                        
                            </Col>
                            <Col>
                            {newProduct ? 
                                !availableNutrients ? (
                                    <h3>loading...</h3>
                                ) : (
                                    availableNutrients.map((nutrient, idx) => {
                                        //let nut=product.nutrients[idx];
                                        return <label className="wrapper">{nutrient.polName} <input key={idx} type="number"
                                            id={nutrient.id} name={nutrient.name} step="0.01"
                                            onChange={(e) => this.handleNutrient(nutrient.id, e, this.state.nutrients)}/> 
                                            <span className="units">{nutrient.unit}</span></label>
                                    })) : 
                                    !availableNutrients ? (
                                        <h3>loading...</h3>
                                    ) : (
                                        nutrients.map((nutrient, idx) => {
                                            const availableNutrient = availableNutrients.find(el => el.id === nutrient.nutrientId);                           
                                            return <label className="wrapper">{availableNutrient.polName} <input key={idx} type="number"
                                                id={nutrient.id} name={availableNutrient.name} step="0.01" defaultValue = {nutrient.amount}
                                                onChange={(e) => this.handleNutrient(nutrient.id, e, this.state.nutrients)}/> 
                                                <span className="units">{availableNutrient.unit}</span></label>
                                        }))
                                }
                            </Col>
                            
                        </Row>
                        <br />
                            <Row>
                                <Col><input type="submit" onClick={this.handleSend} value="Dodaj" /></Col>  
                                <Col><div style = {addedStyle}>{this.state.msg}</div></Col>
                            </Row>
                    </form>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        availableNutrients: state.availableNutrients

    };
};

AddProductPopUp = connect(mapStateToProps)(AddProductPopUp);