import React from "react";
import "./AddRecipePopUp.css";
import { url } from "../../commons/properties"
import { Row, Col} from "react-bootstrap";
import { connect } from "react-redux";


export class AddDietPlanPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            dietPlanId: this.props.dietPlanId,
            dietPlan: null,
            demands: null,
            collaborationId: this.props.collaborationId       
        };
    }


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
        request.open('POST', url + '/dietplan?collaborationId='+collaborationId+'&days='+days+'&meals='+meals, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano plan dietetyczny"
        })
                    
    };


    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
        
        //const availableProducts =  this.state.recipes2;
        let newDietPlan = false;
        let dietPlan= this.state.dietPlan;
        let func= this.handleEdit;
        if(dietPlan === null){
            newDietPlan = true;
            func = this.handleSubmit;
            dietPlan = {
                   
            };
        }

        return (
            <div /*className="modal"*/ >
                <div className="modal_content">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    <form onSubmit={func}>
                    <h3>Dodaj plan dietetyczny:</h3> 
                        <Row >
                            <Col>
                                <label>                         
                                    Liczba dni:
              <input type="number" name="days"  required defaultValue={5} />
                                </label>                        
                            </Col>

                        </Row>
                        <Row >
                            <Col>
                                <label>                         
                                    Liczba posiłków:
              <input type="number" name="meals"  required defaultValue={3} />
                                </label>                        
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

    };
};

AddDietPlanPopUp = connect(mapStateToProps)(AddDietPlanPopUp);