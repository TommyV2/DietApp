import React from "react";
import "./AddRecipePopUp.css";
import { url } from "../../commons/properties"
import { Row, Col, Dropdown, Button, Container } from "react-bootstrap";
import { connect } from "react-redux";
import { AddRecipeToDietPlanPopUp } from "./AddRecipeToDietPlanPopUp";
import { ModifyMealPopUp } from "../../components/diet-plan/ModifyMealPopUp";


export class ModifyDietPlanPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            dietPlanId: this.props.dietPlanId,
            dietPlan: null,
            demands: null,
            day: 1,
            daysNumber: null,
            meals: null,
            mealsNumber: null,
            mealsNumberPerDay: null,
            mealNames: null,
            collaborationId: this.props.collaborationId,
            mealName: null,
            seen: false,
            seen2: false,
            chosenMealId: null,
            chosenDayNumber: null,
            refresh: false
        };
        this.handler = this.handler.bind(this);
    }

    handler() {
        this.setState({
            refresh: true
        })
    }


    setCurrentDay = ( day) => {
        this.setState({
            day: day
        })
    };

    handleClick = () => {
        this.props.toggle();
    };


    success() {
        console.log("Dodano plan dietetyczny!")
    }

    error(err) {
        console.log('Nie dodano planu dietetycznego:', err);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        const collaborationId = this.props.collaborationId;
        const days = event.target.days.value;
        const meals = event.target.meals.value;

        const data = JSON.stringify({
        });

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

    getDaysNumber = (dietPlan) => {
        var daysNumber = 0;
        const meals = dietPlan.meals;
        const length = meals.length;
        if (length !== 0){
            daysNumber = meals[length-1].day;  
        }
        return daysNumber;
    }

    getMealsNumberPerDay = (dietPlan) => {
        var mealsNumber = 0;
        const meals = dietPlan.meals;
        const length = meals.length;
        if (length !== 0){
            mealsNumber = meals[length-1].position;  
        }
        return mealsNumber;
    }

    toggleAddRecipe = (mealId) => {
        this.setState({
            seen: !this.state.seen,
            chosenMealId: mealId,
            refresh: true
        });
    }

    toggleModifyMeal = (mealId, chosenDayNumber, mealName) => {
        this.setState({
            seen2: !this.state.seen2,
            chosenMealId: mealId,
            chosenDayNumber: chosenDayNumber,
            mealName: mealName,
            refresh: true
        });
    }
    
    handleEdit = (event) => {
      /*  event.preventDefault();
        const collaborationId = this.props.collaborationId;
        const text = event.target.text.value;
        const currentDate = new Date();
        const timestamp = currentDate.getTime();
        const sendDate = "2021-05-16T00:00:00";

        const data = JSON.stringify({
            collaborationId: collaborationId,
            sendDate: sendDate,
            text: text
        });

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('PUT', url + '/diet-recommendations/' + this.props.recommendationId, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data); */
        this.props.handler();
       /* this.setState({
            msg: "Edytowano plan"
        }) */

    };

    handleClose = (event) => {
        this.props.toggle();
    };

    async componentDidMount() {
        let dietPlanId = this.state.dietPlanId;
        let dietPlan = null;
        let demands = null;
        let daysNumber = null;
        let mealsNumber = null;
        let mealsNumberPerDay = null;
        let allMealNames = null;
        let mealNames = null;
        let meals = null;

        if (dietPlanId) {
            const response = await fetch(url + '/dietplan/' + dietPlanId);
            dietPlan = await response.json();
            const response2 = await fetch(url + '/dietplan/' + dietPlanId + '/demands');
            demands = await response2.json();
            daysNumber = this.getDaysNumber(dietPlan);
            mealsNumberPerDay = this.getMealsNumberPerDay(dietPlan);
            mealsNumber = dietPlan.meals.length; 
            meals = dietPlan.meals;
            const response3 = await fetch(url + '/dietplan/mealnames');
            allMealNames = await response3.json();
            mealNames = allMealNames[mealsNumberPerDay];          
        }
        this.setState({ dietPlan: dietPlan, demands: demands, daysNumber: daysNumber, mealNames: mealNames , mealsNumber: mealsNumber, meals: meals});
    }

    async componentDidUpdate() {
        if (this.state.refresh === true) {
            let dietPlanId = this.state.dietPlanId;
            let dietPlan = null;
            let demands = null;
            let daysNumber = null;
            let mealsNumber = null;
            let mealsNumberPerDay = null;
            let allMealNames = null;
            let mealNames = null;
            let meals = null;

            if (dietPlanId) {
                const response = await fetch(url + '/dietplan/' + dietPlanId);
                dietPlan = await response.json();
                const response2 = await fetch(url + '/dietplan/' + dietPlanId + '/demands');
                demands = await response2.json();
                daysNumber = this.getDaysNumber(dietPlan);
                mealsNumberPerDay = this.getMealsNumberPerDay(dietPlan);
                mealsNumber = dietPlan.meals.length; 
                meals = dietPlan.meals;
                const response3 = await fetch(url + '/dietplan/mealnames');
                allMealNames = await response3.json();
                mealNames = allMealNames[mealsNumberPerDay];          
            }
            console.log(dietPlan)
            this.setState({ refresh: false, dietPlan: dietPlan, demands: demands, daysNumber: daysNumber, mealNames: mealNames , mealsNumber: mealsNumber, meals: meals});
        }
    }

    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }

        //const availableProducts =  this.state.recipes2;
        let newDietPlan = false;
        let dietPlan = this.state.dietPlan;
        let func = this.handleClose;
      /*  if (dietPlan === null) {
            newDietPlan = true;
            func = this.handleSubmit;
            dietPlan = {

            };
        } */

        let daysNumber = this.state.daysNumber;
        var days = new Array(daysNumber);
        for( var i = 0; i<daysNumber; i++){
            days[i] = i+1;
        }
        var mealsNumber = this.state.mealsNumber;
        var meals = new Array(0);
        for (var i = 0; i < mealsNumber; i++){
            var meal = this.state.dietPlan.meals[i];
            if(meal.day === this.state.day){
                meals.push(meal);
            }
        }

        return (
            <div /*className="modal" */>
                <div className="modal_content_diet_plan">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    <form onSubmit={func}>

                        <h3>Modyfikuj plan dietetyczny:</h3>
                        <Col>
                        <Row><h5 class = "slightly_smaller">Docelowe zapotrzebowanie:</h5></Row>
                        <Row>{this.state.demands ? (
                            <Row xs={2} md={4} lg={6}>
                                <Col>
                                <label >
                                        <h5 class="smaller">kcal: {this.state.demands.kcal} </h5>
                                    </label>
                                </Col>
                                <Col>
                                    <label >
                                        <h5 class="smaller">carbs: {this.state.demands.carbohydrates} </h5>
                                    </label>
                                </Col>
                                <Col>
                                    <label >
                                        <h5 class="smaller">fat: {this.state.demands.fat} </h5>
                                    </label>
                                </Col>
                                <Col>
                                    <label >
                                        <h5 class="smaller">protein: {this.state.demands.protein} </h5>
                                    </label>
                                </Col>
                            </Row>
                        )
                            : (<h5>loading...</h5>)}</Row>
                        </Col>
                        <Row >
                        <Dropdown>
                        <Dropdown.Toggle variant="success" id="dropdown-basic">
                          Dzień {this.state.day}
                        </Dropdown.Toggle>
                      
                        <Dropdown.Menu>
                        {days.map((day) =>  { if (day === this.state.day){
                            return <Dropdown.Item  onClick={() => this.setCurrentDay(day)} active>Dzień {day}</Dropdown.Item>
                        } else {
                            return <Dropdown.Item  onClick={() => this.setCurrentDay(day)}>Dzień {day} </Dropdown.Item>
                        }    
                        }
                        )}                      
                        </Dropdown.Menu>
                      </Dropdown>
                        </Row>
                        <br />
                        {this.state.meals ? (
                            <Row>
                            {this.state.meals.map((meal) => {
                                if(meal.day === this.state.day){
                                    return <Col>
                                    <Row><h5 style= {{fontWeight: "bold"}}class = "slightly_smaller">{this.state.mealNames[meal.position-1]}</h5></Row>
                                    {
                                        meal.recipes ? (meal.recipes.map((recipe) => {return <Row><h5 class="smaller">{recipe.name}</h5> </Row> }))
                                    :(<h5 class="smaller">loading...</h5>)}
                                    <Row><Button size="sm" onClick={() => this.toggleAddRecipe(meal.id)}>Dodaj</Button></Row>
                                    <Row><Button size="sm" onClick={() => this.toggleModifyMeal(meal.id, meal.day,this.state.mealNames[meal.position-1])}>Modyfikuj</Button></Row>
                                    </Col>
                                }
                            
                            })}
                            
                            </Row>
                        ) : (<h5>loading...</h5>)}
                        <Container className='mt-5'/>
                        <Row>
                            <Col><Button size="sm" type="submit" onClick={this.handleSend}>Zapisz</Button></Col>
                            <Col><div style={addedStyle}>{this.state.msg}</div></Col>
                        </Row>
                        
                    </form>
                    {this.state.seen  ? (<AddRecipeToDietPlanPopUp dietPlanId={this.state.dietPlanId} mealId={this.state.chosenMealId}
                        toggle={this.toggleAddRecipe} handler={this.handler} />) : (null)}
                        {this.state.seen2  ? (<ModifyMealPopUp dietPlanId={this.state.dietPlanId} mealId={this.state.chosenMealId}
                            mealName = {this.state.mealName} chosenDayNumber={this.state.chosenDayNumber} toggle={this.toggleModifyMeal} handler={this.handler} />) : (null)}
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {

    };
};

ModifyDietPlanPopUp = connect(mapStateToProps)(ModifyDietPlanPopUp);