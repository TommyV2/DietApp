import React from "react";
import "./AddRecipePopUp.css";
import { url } from "../../commons/properties"
import { Row, Col, Form, InputGroup, FormControl, Button} from "react-bootstrap";
import { connect } from "react-redux";


export class AddRecipePopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            ownerId: this.props.ownerId,
            recipeId: this.props.recipeId,          
            recipe: null,
            products: [],
            products2: [],
            msg: "",
            productName: ""
        };
        this.handleChange = this.handleChange.bind(this);
        this.loadProducts = this.loadProducts.bind(this);
    }


    handleClick = () => {
        this.props.toggle();
    };


    handleProduct(id, event, prod){
        event.preventDefault();
        //let nutrients = this.state.nutrients;
        let products = prod;
        let found = false;  
        const amount = event.target.value;
        
        for(let idx in products){
            console.log(id+": "+products[idx].id)
            if(products[idx].productId === id){
                let product = { 
                    productId: id,
                    amount: amount
                };
                products[idx] = product;
                found = true;
                break; 
            }
        }
        if (found === false){
            let product = { 
                productId: id,
                amount: amount
            };
            products.push(product);
        }
        this.setState({products: products});
        console.log(this.state.products)
    }

    success() {
        console.log("Dodano przepis!")     
    }

    error(err) {
        console.log('Nie dodano przepisu:', err);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        const name = event.target.name.value;
        const instruction = event.target.instruction.value;
        const time = +event.target.time.value;
        const portions = +event.target.portions.value;
        const products = this.state.products;

        const data = JSON.stringify({
            name: name,
            instruction: instruction,
            time: time,
            portions: portions,
            products: products
        });

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('POST', url + '/baserecipe', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano przepis"
        })
                    
    };

    async componentDidMount() {
        const response = await fetch(url + '/product');
        let availableProducts = await response.json();
        availableProducts = availableProducts.slice(0, 5);
        this.props.dispatch({ type: "LOAD_AVAILABLE_PRODUCTS", availableProducts: availableProducts });
        let recipeId = this.state.recipeId;
        let recipe = null;
        if (recipeId){
           const response2 = await fetch(url + '/baserecipe/'+recipeId);
        recipe = await response2.json(); 
        } 
        this.setState({ recipe: recipe });

    }

    async handleChange(event) {
        console.log(event.target.value)  
        const response = await fetch(url + '/product?name='+event.target.value);
        let availableProducts = await response.json();
        availableProducts = availableProducts.slice(0, 5);
        this.props.dispatch({ type: "LOAD_AVAILABLE_PRODUCTS", availableProducts: availableProducts });
        let recipeId = this.state.recipeId;
        let recipe = null;
        if (recipeId){
           const response2 = await fetch(url + '/baserecipe/'+recipeId);
        recipe = await response2.json(); 
        } 
        this.setState({ recipe: recipe });

    }

    async loadProducts(event) {
        event.preventDefault();
        let name = this.state.productName;
        console.log(name);
        const response = await fetch(url + '/product?name='+name);
        const products2 = await response.json();
        this.setState({ products2: products2 });
    }

    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
        const availableProducts = this.props.availableProducts;
        //const availableProducts =  this.state.recipes2;
        let newRecipe = false;
        let recipe= this.state.recipe;
        if(recipe === null){
            newRecipe = true;
            recipe = {
                name: "",
                instruction: "",
                time: "",
                portions: "",
                products: null
            };
        }

        let products = recipe.products;
        return (
            <div /*className="modal" */>
                <div className="modal_content">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    <form onSubmit={this.handleSubmit}>
                        <h3>Dodaj przepis:</h3>
                        <Row >
                            <Col>
                                <label>
                                    Nazwa:
              <input type="text" name="name" required defaultValue={recipe.name}/>
                                </label>
                                <label>
                                    Czas:
              <input type="number" name="time" min="0" required defaultValue={recipe.time} /> 
              <span className="units">min</span>            
                                </label>
                                <label className="wrapper">
                                    Porcje:
              <input type="number" name="portions" min="0" required defaultValue={recipe.portions} />
                                </label>
                                <label className="wrapper">
                                    Instrukcja:
              <input type="text" name="instruction"  required defaultValue={recipe.instruction} />
                                </label>                        
                            </Col>
                            <Col>
                {newRecipe ?            
                (<Form onSubmit={this.loadProducts}>
                    <Form.Group>
                        <InputGroup>
                            <FormControl
                                value={this.state.value}
                                onChange={this.handleChange}
                                placeholder="Produkt"
                                aria-label="Produkt"
                                aria-describedby="basic-addon2"
                            />
                            
                        </InputGroup>
                    </Form.Group>
                </Form>): (<div></div>)}
                
                            {newRecipe ? 
                                !availableProducts ? (
                                    <h3>loading...</h3>
                                ) : (
                                    availableProducts.map((product, idx) => {
                                        //let nut=product.nutrients[idx];
                                        return <label className="wrapper">{product.name} <input key={idx} type="number"
                                            id={product.id} 
                                            onChange={(e) => this.handleProduct(product.id, e, this.state.products)}/> 
                                            </label>
                                    })) : 
                                    !availableProducts ? (
                                        <h3>loading...</h3>
                                    ) : ( 
                                                                          
                                        products.map((product, idx) => {                                           
                                            const availableProduct = availableProducts.find(el => el.id === product.productId);                           
                                            return <label  key={idx} className="wrapper">{product.name} <input key={idx} type="number"
                                                id={product.id}  defaultValue = {product.amount}
                                                onChange={(e) => this.handleProduct(product.id, e, this.state.products)}/> 
                                                </label>
                                        })
                                          
                                        )
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
        availableProducts: state.availableProducts

    };
};

AddRecipePopUp = connect(mapStateToProps)(AddRecipePopUp);