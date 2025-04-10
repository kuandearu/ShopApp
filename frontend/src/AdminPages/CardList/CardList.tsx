import React, { FC } from 'react'
import Card from '../Card/Card'

interface Props {}

const CardList : React.FC<Props> = (props: Props) : JSX.Element => {
  return (
    <div>
        <Card companyName='APPLE' ticker='APPL' price={100}/>
        <Card companyName='MICROSOFT' ticker='MCFT' price={200}/>
        <Card companyName='TESLA' ticker='TSLA' price={300}/>
    </div>
  )
}

export default CardList