import React from "react";
import "./AddRecipePopUp.css";
import { url } from "../../commons/properties"
import { Row, Col, Form, InputGroup, FormControl, Button, Table } from "react-bootstrap";
import { connect } from "react-redux";


export class AddRecipeToDietPlanPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            mealId: this.props.mealId,
            meal: null,
            recipe: null,
            recipes: [],
            recipes2: [],
            msg: "",
            recipeName: "",
            dietPlanId: this.props.dietPlanId

        };
        this.handleChange = this.handleChange.bind(this);
        this.loadRecipes = this.loadRecipes.bind(this);
    }


    handleClick = () => {
        this.props.toggle();
    };

    handleRecipe(recipe, event) {
        event.preventDefault();
        //let nutrients = this.state.nutrients;
        this.setState({ recipe: recipe });
        console.log(this.state.recipe)
    }

    success() {
        console.log("Dodano przepis!")
    }

    error(err) {
        console.log('Nie dodano przepisu:', err);
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const recipe = this.state.recipe;
        recipe.amount = 1;
        const dietPlanId = this.state.dietPlanId;
        const mealId = this.state.mealId;
        const data = JSON.stringify(recipe);

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('POST', url + '/dietplan/' + dietPlanId + '/meal/' + mealId + '/recipe', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano przepis"
        })

    };

    async componentDidMount() {
        const response = await fetch(url + '/baserecipe');
        let availableRecipes = await response.json();
        availableRecipes = availableRecipes.slice(0, 5);
        this.props.dispatch({ type: "LOAD_AVAILABLE_RECIPES", availableRecipes: availableRecipes });
    }

    async handleChange(event) {
        console.log(event.target.value)
        const response = await fetch(url + '/baserecipe?name=' + event.target.value);
        let availableRecipes = await response.json();
        availableRecipes = availableRecipes.slice(0, 5);
        this.props.dispatch({ type: "LOAD_AVAILABLE_RECIPES", availableRecipes: availableRecipes });
    }

    async loadRecipes(event) {
        event.preventDefault();
        let name = this.state.recipeName;
        console.log(name);
        const response = await fetch(url + '/baserecipe?name=' + name);
        const recipes2 = await response.json();
        this.setState({ recipes2: recipes2 });
    }

    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
        const availableRecipes = this.props.availableRecipes;
        //const availableProducts =  this.state.recipes2;
        let newRecipe = false;
        let recipe = this.state.recipe;
        if (recipe === null) {
            newRecipe = true;
            recipe = {
                name: "",
                instruction: "",
                time: "",
                portions: "",
                products: null
            };
        }

        return (
            <div /*className="modal" */>
                <div className="modal_content_diet_plan">
                    <span className="close" onClick={this.handleClick}>
                        &times;
                    </span>
                    <form onSubmit={this.handleSubmit}>
                        <h3>Dodaj przepis:</h3>
                        <Row >
                            <Col>

                                <Form onSubmit={this.loadProducts}>
                                    <Form.Group>
                                        <InputGroup>
                                            <FormControl
                                                value={this.state.value}
                                                onChange={this.handleChange}
                                                placeholder="Przepis"
                                                aria-label="Przepis"
                                                aria-describedby="basic-addon2"
                                            />

                                        </InputGroup>
                                    </Form.Group>
                                </Form>

                                

                                    <Table condensed bordered hover size="sm">
                                        <thead>
                                            <tr>
                                                <td>nazwa</td>
                                                <td>kalorie</td>
                                                <td>węglowodany</td>
                                                <td>białko</td>
                                                <td>tłuszcz</td>
                                                <td></td>
                                            </tr>
                                        </thead>

                                        {
                                            !availableRecipes ? (
                                                <h3>loading...</h3>
                                            ) : (

                                                availableRecipes.map((recipe, idx) => {
                                                    //let nut=product.nutrients[idx];
                                                    return <tr>
                                                        <td>{recipe.name}</td>
                                                        <td>{parseFloat(recipe.kcal)} kcal</td>
                                                        <td>{parseFloat(recipe.carbohydrates).toFixed(2)}g</td>
                                                        <td>{parseFloat(recipe.protein).toFixed(2)}g</td>
                                                        <td>{parseFloat(recipe.fat).toFixed(2)}g</td>
                                                        <td><Button style={{ width: "auto" }} className="wrapper" onClick={(e) => this.handleRecipe(recipe, e)}>Wybierz</Button></td>
                                                    </tr>

                                                }))
                                        }
                                    </Table>
                            </Col>

                        </Row>
                            <br />
                            <Row>
                                <Col><Button size="sm" type="submit" onClick={this.handleSubmit}>Dodaj</Button></Col>
                                <Col><div style={addedStyle}>{this.state.msg}</div></Col>
                            </Row>
                    </form>
                </div>
                </div>
                );
    }
}

const mapStateToProps = (state) => {
    return {
                    availableRecipes: state.availableRecipes

    };
};

                AddRecipeToDietPlanPopUp = connect(mapStateToProps)(AddRecipeToDietPlanPopUp);