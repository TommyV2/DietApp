import React from "react";
import { Row, Card, Accordion, Button } from "react-bootstrap"
import { url } from "../../commons/properties"
import {CustomBanner} from "../../components/CustomBanner.js";

export class DietPlanDay extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            dietPlan: this.props.dietPlan,
            mealsNumberPerDay: this.getMealsNumberPerDay(this.props.dietPlan),
            customIdx: 0,
            refresh: this.props.refresh,
            seen2: false,
            chosenMealId: null,
            chosenDayNumber: null,
            mealName: null,
            mealNames: null
        };
    }

    handler() {
        this.setState({
            refresh: true
        })
    }

    handleAddRecipe(dietPlanId, mealId, event) {
        event.preventDefault();
        //todo
        console.log("todo");
    }


    getMealsNumberPerDay = (dietPlan) => {
        var mealsNumber = 0;
        const meals = dietPlan.meals;
        const length = meals.length;
        var i;
        for (i = 0; i < length; i++) {
            if (meals[i].day === this.props.day) {
                mealsNumber++;
            }
        }
        return mealsNumber;
    }

    async componentDidUpdate() {
        if (this.state.refresh) {
            console.log("updated!");
            this.setState({ refresh: false, mealsNumberPerDay: this.getMealsNumberPerDay(this.props.dietPlan) });
        }
    }

    render() {
        return (
            <>
                <Accordion defaultActiveKey="1">
                    {this.props.dietPlan.meals.map((meal, idx) => {
                        if (meal.day === this.props.day) {
                            return (
                                <Card key={idx} style={{ width: '12rem' }}>
                                    <Card.Header>
                                        <Accordion.Toggle as={Card.Header} variant="link" eventKey={meal.id}>
                                            <p>{this.props.allMealNames[this.state.mealsNumberPerDay][meal.position - 1]}</p>
                                        </Accordion.Toggle>
                                    </Card.Header>
                                    <Accordion.Collapse eventKey={meal.id}>
                                        <Card.Body>
                                            {
                                                meal.recipes ? (meal.recipes.map((recipe) => {
                                                    return (
                                                        <Row><div><h5 class="smaller">{recipe.name} - - - {'>'}  kcal:  {recipe.kcal}</h5></div> </Row>
                                                    )
                                                }))
                                                    : (<h5 class="smaller">loading...</h5>)}
                                            {this.props.type === "specialist" ? (
                                                <div><Button size="sm" onClick={() => this.props.toggle2(meal.id)}>Dodaj przepis</Button>
                                                    <Button size="sm" onClick={() => this.props.toggle(meal.id, meal.day, this.props.allMealNames[this.state.mealsNumberPerDay][meal.position - 1])}>Modyfikuj</Button></div>
                                            ) : (
                                                <></>
                                            )}
                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                            );
                        }

                    })}

                </Accordion>
            </>
        );
    }
}