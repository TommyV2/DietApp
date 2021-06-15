import React from "react";
import { Table, Button } from "react-bootstrap";
import { url } from "../../commons/properties"
import {AddProductPopUp} from "./AddProductPopUp";
import { Col, Row, Form, InputGroup, FormControl } from "react-bootstrap";


export class ProductsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            products: [],
            seen: false,
            refresh: false ,
            product: null,
            productName: ""          
        }
        this.handler= this.handler.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.loadProducts = this.loadProducts.bind(this);
    }
    
      togglePop = (product) => {
        this.setState({
          seen: !this.state.seen,
          product: product,
          refresh: true
        });       
      }

      async deleteProduct(id){
        
        const data = "";          
 
        const putMethod = {
            method: 'DELETE', 
            headers: {
             'Content-type': 'text/plain; charset=UTF-8' 
            },
            body: data 
           }

        await fetch(url+'/product/'+id, putMethod)
        this.setState({
            refresh: true
        })    

      }
    

    async componentDidMount() {
        let name = this.state.productName;
        const response = await fetch(url + '/product?name='+name);
        const products = await response.json();
        this.setState({ products: products });
    }

    async componentDidUpdate() {       
        if (this.state.refresh === true) {
            let name = this.state.productName;           
            const response = await fetch(url + '/product?name='+name);
            const products = await response.json();
            this.setState({ products: products });
            let val = this.state.prev;   
            this.setState({
                refresh: false
              })
        }
    }

    handler(){
        this.setState({
            refresh: true
          })
    }


    handleChange(event) {
        this.setState({ productName: event.target.value });
    }

    async loadProducts(event) {
        event.preventDefault();
        let name = this.state.productName;
        console.log(name);
        const response = await fetch(url + '/product?name='+name);
        const products = await response.json();
        this.setState({ products: products });
    }

    render() {
        //const clickedProduct = this.state.product;
        let products = this.state.products.map((product, idx) => {
            return (
                <tr>
                <td>{product.id}</td>
                <td>{product.source}</td>
                <td>{product.name}</td>
                <td>{product.kcal} kcal</td>
                <td>{product.carbohydrates}g</td>
                <td>{product.sugar} g</td>
                <td>{product.fat} g</td>
                <td>{product.saturatedFat} g</td>
                <td>{product.protein} g</td>
                <td>{product.fiber} g</td>
                <td>
                    <Button size="sm"  onClick={() => this.togglePop(product)}>Podgląd</Button>
                    <Button className="del" size="sm" onClick={() => this.deleteProduct(product.id)}>Usuń</Button>
                </td>
            </tr>
            );
        });

        return (
            <div>
            <Col>
                <Row>
                <Form onSubmit={this.loadProducts}>
                <Form.Group>
                    <InputGroup>
                        <FormControl
                            value={this.state.value}
                            onChange={this.handleChange}
                            placeholder="Produkt"
                            aria-label="Produkt"
                            aria-describedby="basic-addon2"
                        />
                        <InputGroup.Append>
                            <Button variant="outline-secondary" type="submit" value="Szukaj">Szukaj</Button>
                        </InputGroup.Append>
                    </InputGroup>
                </Form.Group>
            </Form>
                </Row>
                <Row>
                <Table striped hover>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Źródło</th>
                            <th>Produkt</th>
                            <th>kcal</th>
                            <th>Węglowodany</th>
                            <th>Cukier</th>
                            <th>Tłuszcz</th>
                            <th>Tłuszcze nasycone</th>
                            <th>Białko</th>
                            <th>Błonnik</th>
                            <th>Edycja</th>
                        </tr>
                    </thead>
                    <tbody>

                        {products}

                        <tr>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>
                                <Button size="sm" onClick={() => this.togglePop(null)}>Dodaj</Button>
                            </td>
                            
                        </tr>
                    </tbody>
                </Table>
                </Row>
                </Col>
                {this.state.seen ? <AddProductPopUp ownerId="1" productId = {this.state.product ? (this.state.product.id) : null} 
                product={this.state.product} toggle={this.togglePop} handler = {this.handler}/> : null}
            </div>
        );
    }
}