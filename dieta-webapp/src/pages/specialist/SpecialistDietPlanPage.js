import React from "react";
import { NavigationBar } from "../../components/NavigationBar";
import {
  Container,
  Row,
  CardDeck,
  Alert,
  Col,
  Carousel,
  Button,
} from "react-bootstrap";
import { url } from "../../commons/properties";
import { DietPlanDay } from "../../components/diet-plan/DietPlanDay";
import { CustomBanner } from "../../components/CustomBanner.js";
import { ModifyMealPopUp } from "./ModifyMealPopUp";
import { AddRecipeToDietPlanPopUp } from "./AddRecipeToDietPlanPopUp";
import "./SpecialistDietPage.css";

export class SpecialistDietPlanPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      collaborationId: this.props.match.params.collaborationId,
      dietPlanId: this.props.match.params.dietPlanId,
      customerData: null,
      daysNumber: null,
      weeks: null,
      allMealNames: null,
      meals: null,
      maxDaysToShow: 7,
      refresh: false,
      seen: false,
      seen2: false,
      mealId: null,
      chosenDayNumber: null,
      mealName: null,
    };
    this.toggleModifyMeal = this.toggleModifyMeal.bind(this);
    this.toggleAddRecipe = this.toggleAddRecipe.bind(this);
    this.handler = this.handler.bind(this);
  }

  handler() {
    this.setState({
      refresh: true,
    });
  }

  toggleAddRecipe = (mealId) => {
    this.setState({
      seen: !this.state.seen,
      chosenMealId: mealId,
      refresh: true,
    });
  };

  toggleModifyMeal = (mealId, chosenDayNumber, mealName) => {
    this.setState({
      seen2: !this.state.seen2,
      chosenMealId: mealId,
      chosenDayNumber: chosenDayNumber,
      mealName: mealName,
      refresh: true,
    });
  };

  handleAddMeal(day, event) {
    event.preventDefault();
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json; charset=UTF-8" },
    };
    fetch(
      url + "/dietplan/" + this.state.dietPlanId + "/meal?day=" + day,
      requestOptions
    )
      .then(async (response) => {
        const data = await response.json();
        if (!response.ok) {
          const error = (data && data.error) || response.statusText;
          return Promise.reject(error);
        }
        const response4 = await fetch(url + "/dietplan/mealnames");
        let allMealNames = await response4.json();
        this.setState({ refresh: true, allMealNames: allMealNames });
      })
      .catch((error) => {
        //todo
        console.log("error");
      });
  }

  getDaysNumber = (dietPlan) => {
    var daysNumber = 0;
    const meals = dietPlan.meals;
    const length = meals.length;
    var i;
    if (length != 0) {
      for (i = 0; i < length; i++) {
        if (meals[i].day > daysNumber) {
          daysNumber = meals[i].day;
        }
      }
    }
    return daysNumber;
  };

  async componentDidMount() {
    let collaborationData;
    let customerData;
    let dietPlan = null;
    let daysNumber = null;
    let allMealNames = null;
    let meals = null;

    if (this.state.dietPlanId) {
      const response = await fetch(
        url + "/collaborations/" + this.state.collaborationId
      );
      collaborationData = await response.json();
      const response2 = await fetch(
        url + "/customers/" + collaborationData.customerId
      );
      customerData = await response2.json();
      const response3 = await fetch(url + "/dietplan/" + this.state.dietPlanId);
      dietPlan = await response3.json();
      daysNumber = this.getDaysNumber(dietPlan);
      const response4 = await fetch(url + "/dietplan/mealnames");
      allMealNames = await response4.json();
      console.log(allMealNames);
      meals = dietPlan.meals;
      //suma
    }
    this.setState({
      refresh: false,
      customerData: customerData,
      daysNumber: daysNumber,
      allMealNames: allMealNames,
      meals: meals,
      dietPlan: dietPlan,
    });
  }

  async componentDidUpdate() {
    if (this.state.refresh === true) {
      let collaborationData;
      let customerData;
      let dietPlan = null;
      let daysNumber = null;
      let allMealNames = null;
      let meals = null;

      if (this.state.dietPlanId) {
        const response = await fetch(
          url + "/collaborations/" + this.state.collaborationId
        );
        collaborationData = await response.json();
        const response2 = await fetch(
          url + "/customers/" + collaborationData.customerId
        );
        customerData = await response2.json();
        const response3 = await fetch(
          url + "/dietplan/" + this.state.dietPlanId
        );
        dietPlan = await response3.json();
        daysNumber = this.getDaysNumber(dietPlan);
        const response4 = await fetch(url + "/dietplan/mealnames");
        allMealNames = await response4.json();
        console.log(allMealNames);
        meals = dietPlan.meals;
      }
      this.setState({
        refresh: false,
        customerData: customerData,
        daysNumber: daysNumber,
        allMealNames: allMealNames,
        meals: meals,
        dietPlan: dietPlan,
      });
    }
  }

  getSummary = (day, keyword) => {
    var filtered = this.state.meals.filter((m) => m.day == day);
    var kcal = 0;
    var fat = 0;
    var carbo = 0;
    var protein = 0;
    for (var i = 0; i < filtered.length; i++) {
      for (var k = 0; k < filtered[i].recipes.length; k++) {
        var recipe = filtered[i].recipes[k];
        kcal += recipe.kcal;
        fat += recipe.fat;
        carbo += recipe.carbohydrates;
        protein += recipe.protein;
      }
    }
    kcal = Math.round(kcal * 100) / 100;
    fat = Math.round(fat * 100) / 100;
    carbo = Math.round(carbo * 100) / 100;
    protein = Math.round(protein * 100) / 100;

    var summary = {
      kcal: kcal,
      protein: protein,
      fat: fat,
      carbo: carbo,
    };
    return summary[keyword];
  };

  render() {
    let daysNumber = this.state.daysNumber;
    var days = new Array(daysNumber);
    for (var i = 0; i < daysNumber; i++) {
      days[i] = i + 1;
    }

    let weeksNumber = Math.ceil(daysNumber / this.state.maxDaysToShow);
    var weeks = new Array(weeksNumber);
    for (var i = 0; i < weeksNumber; i++) {
      weeks[i] = i;
    }

    return (
      <div>
        <NavigationBar type="specialist" />
        <Container>
          <Row className="justify-content-md-center">
            <Col xs lg="8">
              <Alert variant="success">
                <center>
                  <div>
                    Plan dietetyczny dla użytkownika{" "}
                    {this.state.customerData ? (
                      <b>{this.state.customerData.name}</b>
                    ) : (
                      <h5>loading...</h5>
                    )}
                  </div>
                </center>
              </Alert>
            </Col>
          </Row>
        </Container>

        <Container fluid>
          <Carousel interval={null}>
            {weeks.map((week) => (
              <Carousel.Item>
                <Row className="justify-content-md-center">
                  <CardDeck>
                    {days
                      .slice(
                        week * this.state.maxDaysToShow,
                        (week + 1) * this.state.maxDaysToShow
                      )
                      .map((day) => (
                        <div>
                          {this.state.customerData ? (
                            <CustomBanner text={"Dzień " + day} />
                          ) : (
                            <h5>loading...</h5>
                          )}
                          <DietPlanDay
                            type="specialist"
                            day={day}
                            toggle={this.toggleModifyMeal}
                            toggle2={this.toggleAddRecipe}
                            allMealNames={this.state.allMealNames}
                            dietPlan={this.state.dietPlan}
                            refresh={true}
                          />
                          <Container className="mt-3" />
                          <Container>
                            <Row className="justify-content-md-center">
                              <Col xs lg="8">
                                <center>
                                  <Button
                                    size="sm"
                                    onClick={(event) =>
                                      this.handleAddMeal(day, event)
                                    }
                                  >
                                    Dodaj posiłek
                                  </Button>
                                </center>
                              </Col>
                            </Row>
                          </Container>
                          <Container className="mt-3">
                            <Row className="justify-content-md-center">
                              {"Kcal: " + this.getSummary(day, "kcal")}
                            </Row>
                            <Row className="justify-content-md-center">
                              {"B: " + this.getSummary(day, "protein")}
                            </Row>
                            <Row className="justify-content-md-center">
                              {"W: " + this.getSummary(day, "carbo")}
                            </Row>
                            <Row className="justify-content-md-center">
                              {"T: " + this.getSummary(day, "fat")}
                            </Row>
                          </Container>
                        </div>
                      ))}
                  </CardDeck>
                </Row>
                <Row>
                  <Container className="mt-5" />
                </Row>
              </Carousel.Item>
            ))}
          </Carousel>
        </Container>
        {this.state.seen ? (
          <AddRecipeToDietPlanPopUp
            dietPlanId={this.state.dietPlanId}
            mealId={this.state.chosenMealId}
            toggle={this.toggleAddRecipe}
            handler={this.handler}
          />
        ) : null}
        {this.state.seen2 ? (
          <ModifyMealPopUp
            dietPlanId={this.state.dietPlan.id}
            mealId={this.state.chosenMealId}
            mealName={this.state.mealName}
            chosenDayNumber={this.state.chosenDayNumber}
            toggle={this.toggleModifyMeal}
            handler={this.handler}
          />
        ) : null}
      </div>
    );
  }
}
