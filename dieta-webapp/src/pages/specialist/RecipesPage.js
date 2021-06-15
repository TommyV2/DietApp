import React from "react";
import { Table, Button } from "react-bootstrap";
import { url } from "../../commons/properties"
import {AddRecipePopUp} from "./AddRecipePopUp";
import { Col, Row, Form, InputGroup, FormControl } from "react-bootstrap";


export class RecipesPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            recipes: [],
            seen: false,
            refresh: false ,
            recipe: null,
            recipeName: "",
            new_recipe: false         
        }
        this.handler= this.handler.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.loadRecipes = this.loadRecipes.bind(this);
    }
    
      togglePop = (recipe, is_new_recipe) => {
        this.setState({
          seen: !this.state.seen,
          recipe: recipe,
          refresh: true,
          new_recipe: is_new_recipe
        });       
      }
     

      async deleteRecipe(id){
        
        const data = "";          
 
        const putMethod = {
            method: 'DELETE', 
            headers: {
             'Content-type': 'text/plain; charset=UTF-8' 
            },
            body: data 
           }

        await fetch(url+'/baserecipe/'+id, putMethod)
        this.setState({
            refresh: true
        })    

      }
    

    async componentDidMount() {
        let name = this.state.recipeName;
        const response = await fetch(url + '/baserecipe?name='+name);
        const recipes = await response.json();
        this.setState({ recipes: recipes });
    }

    async componentDidUpdate() {       
        if (this.state.refresh === true) {
            let name = this.state.recipeName;           
            const response = await fetch(url + '/baserecipe?name='+name);
            const recipes = await response.json();
            this.setState({ recipes: recipes });
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
        console.log(event.target.value)
        this.setState({ recipeName: event.target.value });
    }

    async loadRecipes(event) {
        event.preventDefault();
        let name = this.state.recipeName;
        console.log(name);
        const response = await fetch(url + '/baserecipe?name='+name);
        const recipes = await response.json();
        this.setState({ recipes: recipes });
    }

    render() {
        let recipes = this.state.recipes.map((recipe, idx) => {
            return (
                <tr>
                <td>{recipe.id}</td>
                <td>{recipe.name}</td>
                <td>{recipe.time} min</td>
                <td>{recipe.portions}</td>
                <td>
                    <Button size="sm"  onClick={() => this.togglePop(recipe, false)}>Podgląd</Button>
                    <Button className="del" size="sm" onClick={() => this.deleteRecipe(recipe.id)}>Usuń</Button>
                </td>
            </tr>
            );
        });

        return (
            <div>
            <Col>
                <Row>
                <Form onSubmit={this.loadRecipes}>
                <Form.Group>
                    <InputGroup>
                        <FormControl
                            value={this.state.value}
                            onChange={this.handleChange}
                            placeholder="Przepis"
                            aria-label="Przepis"
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
                            <th>Przepis</th>
                            <th>Czas</th>
                            <th>Porcje</th>
                            <th>Edycja</th>
                        </tr>
                    </thead>
                    <tbody>

                        {recipes}

                        <tr>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                            <td>..</td>
                                                      
                            <td>
                                <Button size="sm" onClick={() => this.togglePop(null, true)}>Dodaj</Button>
                            </td>
                            
                        </tr>
                    </tbody>
                </Table>
                </Row>
                </Col>
                {this.state.seen ? <AddRecipePopUp ownerId="1" new_recipe = {this.state.new_recipe} recipeId = {this.state.recipe ? (this.state.recipe.id) : null} 
                product={this.state.recipe} toggle={this.togglePop} handler = {this.handler}/> : null}
            </div>
        );
    }
}