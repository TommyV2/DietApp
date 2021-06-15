import React from "react";
import "./ModifyMealPopUp.css";
import { url } from "../../commons/properties"
import { Row, Col, Dropdown, Button, Container, Table } from "react-bootstrap";
import { connect } from "react-redux";
//import { AddRecipeToDietPlanPopUp } from "./AddRecipeToDietPlanPopUp";


export class ModifyMealPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            dietPlanId: this.props.dietPlanId,
            mealId: this.props.mealId,
            dayNumber: this.props.chosenDayNumber,
            mealName: this.props.mealName,
            meal: null,
            recipes: null,
            seen: false,
            chosenRecipeId: null,
            refresh: false
        };
        this.handler = this.handler.bind(this);
    }

    handler() {
        this.setState({
            refresh: true
        })
    }


    handleClick = () => {
        this.props.toggle(this.state.mealId, this.state.dayNumber,this.state.mealName)
    };


    success() {
        console.log("Sukces!")
    }

    error(err) {
        console.log('Niepowodzenie', err);
    }

    async deleteRecipe(event, recipeId) {
        event.preventDefault();
        const data = JSON.stringify({});
        let recipes = this.state.recipes;
        for (var idx in this.state.recipes){
            var recipe = this.state.recipes[idx];
            if (recipe.id === recipeId){
                recipes.splice(idx, 1);
                break;    
            }
        }
        /*
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('DELETE', url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        var meal = null;
        */
        var newMeal = null;
        var newRecipes = null
        await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, {
            method: 'DELETE',       
            body: data
        })

        const response = await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/'+this.state.mealId);
        newMeal = await response.json();
        if (newMeal.recipes){
            newRecipes = newMeal.recipes;
        }

        this.setState({ meal: newMeal, recipes: newRecipes, refresh: false, msg: "Usunięto przepis"});

    }
    
    async modifyRecipe(event, recipe){
        console.log(recipe.id)

        let recipeId = recipe.id;
        let recipeAmount = document.querySelector("[id^=r"+recipeId+"]");
        let productAmounts = document.querySelectorAll("[id^=p"+recipeId+"]");
        let modifiedRecipe = recipe;
        modifiedRecipe.amount = parseFloat(recipeAmount.innerText)
        /*
        for(var i in modifiedRecipe.products){
            modifiedRecipe.products[i].amount = (parseFloat(productAmounts[i].innerText.slice(0,-1));
        } */
        let recipes = this.state.recipes;
        for (var idx in this.state.recipes){
            var rec = this.state.recipes[idx];
            if (rec.id === recipeId){
                recipes[idx] = modifiedRecipe;
                break;    
            }
        }

        
        const data = JSON.stringify(modifiedRecipe);
        /*
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('PUT', url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);  */

        var newMeal = null;
        var newRecipes = null
        await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, {
            method: 'PUT', 
            headers: {
                'Content-type': 'application/json; charset=UTF-8' 
            },      
            body: data
        })
        const response = await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/'+this.state.mealId);
        newMeal = await response.json();
        if (newMeal.recipes){
            newRecipes = newMeal.recipes;
        }
        
        this.setState({
            meal: newMeal,
            recipes: newRecipes,
            refresh: false,
            msg: "Modifkowano posiłek"
        })   
        
    }

    async deleteProduct(event, recipeId, productId){
        console.log(productId)
        var recipe = null;
        var recipes = this.state.recipes;
        var del = false;
        for (var idx in recipes){
            recipe = recipes[idx];
            if (recipe.id === recipeId){
                for (var id in recipe.products){
                    var product = recipe.products[id];
                    if(product.id === productId){
                        if (recipe.products.length === 1){
                            del = true
                        } else {
                            recipe.products.splice(id, 1);
                        }
                        break;
                    }
                }
               recipes[idx] = recipe; 
               break;
            }
            
        }
        if (del === false){
            const data = JSON.stringify(recipe);
            /*
            console.log(recipe)
            var request = new XMLHttpRequest();
            request.onload = this.success;
            request.onerror = this.error;
            request.open('PUT', url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, true);
            request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
            request.send(data);
            console.log(recipe) */
            
            await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' 
                },          
                body: data
            })
        } else{
            const data = JSON.stringify({});
            let recipes = this.state.recipes;
            for (var ide in this.state.recipes){
                var rec = this.state.recipes[ide];
                if (rec.id === recipeId){
                    recipes.splice(idx, 1);
                    break;    
                }
            }
            /*
            var request2 = new XMLHttpRequest();
            request2.onload = this.success;
            request2.onerror = this.error;
            request2.open('DELETE', url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, true);
            request2.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
            request2.send(data); */
            await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/' + this.state.mealId + '/recipe/' + recipeId, {
                method: 'DELETE',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' 
                },          
                body: data
            })
        }
        
        var newMeal = null;
        var newRecipes = null
        const response = await fetch(url + '/dietplan/'+this.state.dietPlanId+'/meal/'+this.state.mealId);
        newMeal = await response.json();
        if (newMeal.recipes){
            newRecipes = newMeal.recipes;
        }
        this.setState({
            meal: newMeal,
            recipes: newRecipes,
            refresh: false,
            msg: "Modifkowano przepis"
        })

    }

    handleSubmit = (event) => {
        event.preventDefault();
        const collaborationId = this.props.collaborationId;
        const days = event.target.days.value;
        const meals = event.target.meals.value;

        const data = JSON.stringify();

        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('POST', url + '/dietplan?collaborationId=' + collaborationId + '&days=' + days + '&meals=' + meals, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano plan dietetyczny"
        })

    };
    
    handleEdit = (event) => {   
        this.props.handler();
    };

    handleClose = (event) => {
        this.props.toggle();
    };

    async componentDidMount() {
        let dietPlanId = this.state.dietPlanId;
        let mealId = this.state.mealId;
        let meal = null;
        let recipes = null;

        if (dietPlanId && mealId) {
            const response = await fetch(url + '/dietplan/'+dietPlanId+'/meal/'+mealId);
            meal = await response.json();
            if (meal.recipes){
                recipes = meal.recipes;
            }
                     
        }
        this.setState({ meal: meal, recipes: recipes});
    }

    async componentDidUpdate() {
        if (this.state.refresh === true) {
            let dietPlanId = this.state.dietPlanId;
            let mealId = this.state.mealId;
            let meal = null;
            let recipes = null;

            if (dietPlanId && mealId) {
                const response = await fetch(url + '/dietplan/'+dietPlanId+'/meal/'+mealId);
                meal = await response.json();
                if (meal.recipes){
                    recipes = meal.recipes;
                    console.log(recipes)
                }       
            }
            this.setState({ meal: meal, recipes: recipes, refresh: false});
        }
    }

    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
               
        return (
            <div /*className="modal" */>
                <div className="modal_content_meal">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    

                        <Row>
                        <Col><h3>Modyfikuj posiłek:</h3></Col>
                        <Col><h3>Dzień: {this.state.dayNumber} {this.state.mealName}</h3></Col>
                        </Row>
            <Table condensed bordered hover size="sm">
                <thead>
                    <tr> 
                    <th>Potrawa</th>
                    <th>Ilość (porcje)</th>
                    <th>Ilość</th>
                    <th>kcal</th>
                    <th>Węglowodany</th>
                    <th>Białko</th>
                    <th>Tłuszcz</th>
                    <th>Błonnik</th>
                    <th></th>
                    <th></th> 
                    </tr>
                </thead>
                <tbody>          
                {this.state.meal ? ( this.state.meal.recipes.map((recipe) => {
                   return<> 
                   <tr class="recipe">
                        <td>{recipe.name}</td>
                        <td id={"r"+recipe.id} class="editable" contenteditable='true'>{recipe.amount}</td>
                        <td>{recipe.amount*recipe.portion}g</td>
                        <td>{parseFloat(recipe.amount*(recipe.portion/100)*recipe.kcal)} kcal</td>
                        <td>{parseFloat(recipe.amount*(recipe.portion/100)*recipe.carbohydrates).toFixed(2)}g</td>
                        <td>{parseFloat(recipe.amount*(recipe.portion/100)*recipe.protein).toFixed(2)}g</td>
                        <td>{parseFloat(recipe.amount*(recipe.portion/100)*recipe.fat).toFixed(2)}g</td>
                        <td>{parseFloat(recipe.amount*(recipe.portion/100)*recipe.fiber).toFixed(2)}g</td>
                        <td><Button onClick={(event) => this.deleteRecipe(event, recipe.id)}>Usuń</Button></td>
                        <td><Button onClick={(event) => this.modifyRecipe(event, recipe)}>Modyfikuj</Button></td>     
                    </tr>    
                        {recipe.products ? (recipe.products.map((product) => {
                            return <tr>
                            <td>{product.name}</td>
                            <td></td>
                            <td /* id={"p"+recipe.id+':'+product.id} class="editable" contenteditable='true'*/>{recipe.amount*product.amount}g</td>
                            <td>{parseFloat(recipe.amount*(product.amount/100)*product.kcal)} kcal</td>
                            <td>{parseFloat(recipe.amount*(product.amount/100)*product.carbohydrates).toFixed(2)}g</td>
                            <td>{parseFloat(recipe.amount*(product.amount/100)*product.protein).toFixed(2)}g</td>
                            <td>{parseFloat(recipe.amount*(product.amount/100)*product.fat).toFixed(2)}g</td>
                            <td>{parseFloat(recipe.amount*(product.amount/100)*product.fiber).toFixed(2)}g</td>
                            <td><Button onClick={(event) => this.deleteProduct(event, recipe.id, product.id)}>Usuń</Button></td>
                            </tr>
                        })) : (<h3>loading...</h3>)}
                        </> 
                                             
                    
                })): (<h3>loading...</h3>)}
                    
                </tbody>
                </Table>
                    <Button onClick={(event) => this.props.toggle()}>Zamknij</Button>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {

    };
};

ModifyMealPopUp = connect(mapStateToProps)(ModifyMealPopUp);